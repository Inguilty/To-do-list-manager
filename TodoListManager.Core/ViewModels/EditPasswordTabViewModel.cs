using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Binding.Combiners;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class EditPasswordTabViewModel : BaseViewModel<PasswordContent>
    {
        public EditPasswordTabViewModel(IMvxNavigationService navigationService, IDbService dbService)
        : base(navigationService)
        {
            _navigationService = navigationService;
            _dataService = dbService;
        }

        #region Fields and properties    
        public bool IsNewPasswordValid = false;
        public bool IsOldPasswordValid = false;
        public bool ConfirmValid = false;
        private string _oldPassword;
        private string _newPassword;
        private string _alertMessage;
        private string _confirmNewPassword;
        private UIColor _alertColor;
        private readonly IMvxNavigationService _navigationService;
        private IDbService _dataService;
        private bool _result = false;
        private UserModel _user;
        private ProfileTabViewModel _model;
        public UIColor AlertColor
        {
            get => _alertColor;
            set
            {
                _alertColor = value;
                RaisePropertyChanged();
            }
        }
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                RaisePropertyChanged();
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
            }
        }

        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand CancelCommand => new MvxCommand(Cancel);

        private void Cancel()
        {
            ViewDispose(this);
            _result = false;
            _model.Result += () => _result;
            _model.Begin();
        }

        public override void Prepare(PasswordContent parameter)
        {
            _user = parameter.User;
            _model = parameter.Profile;
        }
        #region Validation
        public void CheckOldPassword()
        {
            if (!string.IsNullOrEmpty(_oldPassword))
            {
                var user = _dataService.Query<UserModel>("SELECT * FROM Users WHERE Password = ? AND Login = ?", _oldPassword, _user.Login).SingleOrDefault();
                if (user == null)
                {
                    AlertColor = UIColor.Red;
                    AlertMessage = "Old password is incorrect!";
                    IsOldPasswordValid = false;
                }
                else
                    IsOldPasswordValid = user.Password == OldPassword;

                if (IsOldPasswordValid)
                {
                    AlertMessage = "";
                }
                else
                {
                    AlertColor = UIColor.Red;
                    AlertMessage = "Old password is incorrect";
                }
            }
            else
            {
                AlertColor = UIColor.Red;
                AlertMessage = "Enter your old password!";
                IsOldPasswordValid = false;
            }
        }
        public void IsPasswordValid()
        {
            if (_newPassword.Length >= 6 && _newPassword == NewPassword && _newPassword.Length <= 36)
            {
                IsNewPasswordValid = true;
                AlertMessage = "";
            }
            else
                if (_newPassword.Length < 6)
            {
                IsNewPasswordValid = false;
                AlertColor = UIColor.Red;
                AlertMessage =
                    "Password length must be more than 6 characters.";
            }
            else
                if (_newPassword.Length > 36)
            {
                IsNewPasswordValid = false;
                AlertColor = UIColor.Red;
                AlertMessage =
                    "Password length must be less than 36 characters.";
            }

        }
        public void CheckConfirmPassword()
        {
            if (IsNewPasswordValid)
            {
                if (_newPassword != _confirmNewPassword)
                {
                    ConfirmValid = false;
                    AlertColor = UIColor.Red;
                    AlertMessage = "The password you entered do not match!";
                }
                else
                {
                    ConfirmValid = true;
                    AlertMessage = "";
                }
            }
        }

        public override void Validate()
        {
            var equal = _newPassword == _confirmNewPassword;
            var user = _dataService.GetItem<UserModel>(_user.Id);

            if ((_oldPassword == user.Password) && (_newPassword?.Length >= 6) && (_newPassword?.Length <= 36) && equal)
            {
                user.Password = NewPassword;
                _user.Password = NewPassword;
                AlertMessage = "";
                AlertColor = UIColor.Green;
                _dataService.SaveItem<UserModel>(user);
                ViewDispose(this);
                _result = true;
                _model.Result += () => _result;
                _model.Begin();
                return;
            }
            if (string.IsNullOrEmpty(_newPassword) || string.IsNullOrEmpty(_oldPassword) || string.IsNullOrEmpty(_confirmNewPassword))
            {
                AlertColor = UIColor.Red;
                AlertMessage = "Not all required fields was filled!";
                _result = false;
                _model.Result += () => _result;
                _model.Begin();
                return;
            }
            if (_oldPassword != user.Password)
            {
                AlertColor = UIColor.Red;
                AlertMessage = "Old password is wrong!";
                _result = false;
                _model.Result += () => _result;
                _model.Begin();
                return;
            }
            if ((_newPassword?.Length < 6) || (_newPassword?.Length > 36))
            {
                AlertColor = UIColor.Red;
                AlertMessage = "New password does not fit!";
                _result = false;
                _model.Result += () => _result;
                _model.Begin();
                return;
            }
            if (_newPassword != _confirmNewPassword)
            {
                AlertColor = UIColor.Red;
                AlertMessage = "New and confirm password do not match!";
                _result = false;
                _model.Result += () => _result;
                _model.Begin();
            }
        }
        #endregion      
    }
}
