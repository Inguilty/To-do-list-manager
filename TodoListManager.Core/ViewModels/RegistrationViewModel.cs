using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListManager.Core.Enums;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel<UserModel>
    {
        public RegistrationViewModel(IFilePickerService filePicker, IDbService dataService, IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _filePicker = filePicker;
            _dataService = dataService;
            Title = "Registration";
        }

        #region Fields and Properties
        private readonly IDbService _dataService;
        private readonly IFilePickerService _filePicker;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private byte[] _profilePicture;
        private bool _isValid;
        private UserModel _user;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }
        public byte[] ProfilePicture
        {
            get => _profilePicture;
            set
            {
                _profilePicture = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public override void Prepare(UserModel parameter)
        {
            _user = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public ICommand SubmitCommand => new MvxCommand(SubmitRegistration);
        public ICommand ChooseFileCommand => new MvxAsyncCommand(ChooseFile);

        private async Task ChooseFile()
        {
            var pickedFileModel = await _filePicker.NewUserUploadImageAsync();
            _profilePicture = pickedFileModel.ImageBytes;
            OnUploadImageFinally();
        }
        public delegate void UploadImageEventHandler();
        public event ProfileTabViewModel.UploadImageEventHandler UploadImageFinally;

        protected virtual void OnUploadImageFinally()
        {
            UploadImageFinally?.Invoke();
        }

        private void SubmitRegistration()
        {
            if (_isValid)
            {
                var newUser = new UserModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email.ToLower(),
                    Login = this.Login.ToLower(),
                    Password = this.Password,
                    userType = UserType.User,
                    photo = this.ProfilePicture
                };
                _dataService.SaveItem(newUser);
                ViewDispose(this);
            }           
        }

        #region  Validation
        public override void Validate()
        {
            if (!string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password) && !string.IsNullOrWhiteSpace(_email))
            {
                _isValid = IsPasswordValid();
                if (!_isValid)
                    AlertMessage = "The password is invalid. The length must be more than 6 and less than 36 characters.";
            }
            else
            {
                AlertMessage = "Not all required fields are filled!";
            }
        }

        private bool IsPasswordValid()
        {
            return _password.Length >= 6 && _password == Password && _password.Length <= 36;
        }
        public void IsPasswordLengthValid()
        {
            if (_password.Length >= 6 && _password == Password && _password.Length <= 36)
            {
                AlertMessage = "";
            }
            else
            if (_password.Length < 6)
            {
                AlertMessage =
                    "Password length must be more than 6 characters.";
            }
            else
            if (_password.Length > 36)
            {
                AlertMessage =
                    "Password length must be less than 36 characters.";
            }

        }
        public void CheckLogin()
        {
            AlertMessage = "";
            if (string.IsNullOrWhiteSpace(Login))
                return;

            var user = _dataService.Query<UserModel>("SELECT * FROM Users WHERE Login = ?", _login).SingleOrDefault();

            AlertMessage = user == null ? "" : "User with such login is already exist!";
        }

        public void CheckEmail()
        {
            AlertMessage = "";
            if (string.IsNullOrWhiteSpace(Email))
                return;

            if (!Regex.Match(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                AlertMessage = "E-mail address is invalid!";
                return;
            }

            var user = _dataService.Query<UserModel>("SELECT * FROM Users WHERE Email = ?", _email).SingleOrDefault();
            AlertMessage = user != null ? AlertMessage = "User with such a mail is already registered!" : "";
        }
        #endregion
    }
}

