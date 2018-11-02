using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class ProfileTabViewModel : BaseViewModel<UserModel>
    {
        public ProfileTabViewModel(IMvxNavigationService navigationService, IFilePickerService service, IDbService dataService,UserModel user)
            : base(navigationService)
        {
            Title = "Profile";
            _dataService = dataService;
            _filePicker = service;
            _user = user;
            InitializeUserData();
        }

        #region Fields and properties
        private string _firstName;
        private string _lastName;
        private string _email;
        private UIColor _alertColor;
        private bool _emailValid = false;
        public bool ChangesResult = false;
        private byte[] _profileImage;
        private readonly IDbService _dataService;
        private readonly IFilePickerService _filePicker;
        private UserModel _user;

        public UIColor AlertColor
        {
            get => _alertColor;
            set
            {
                _alertColor = value;
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
        public byte[] ProfileImage
        {
            get => _profileImage;
            set
            {
                _profileImage = value;
                RaisePropertyChanged();
            }
        }
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
        #endregion

        public void InitializeUserData()
        {
            _firstName = _user.FirstName;
            _lastName = _user.LastName;
            _profileImage = _user.photo;
            _email = _user.Email.ToLower();
        }

        public ICommand SaveCommand => new MvxCommand(Validate);
        public ICommand ChangePasswordCommand => new MvxCommand(ChangePassword);

        public event ChangePasswordResult Result;

        //public void Begin()
        //{
        //    if (Result != null && Result.Invoke())
        //    {
        //        AlertMessage = "Password has been successfully changed!";
        //        AlertColor = UIColor.Green;
        //    }
        //    else
        //    {
        //        AlertMessage = "";
        //        AlertColor = UIColor.Red;
        //    }
        //}

        private void ChangePassword()
        {
            //var cls = this;
            NavigationService.Navigate<EditPasswordTabViewModel, UserModel > (_user);
        }

        public ICommand UploadImageCommand => new MvxAsyncCommand(UploadImageAsync);

        private async Task UploadImageAsync()
        {
            var pickedFileModel = await _filePicker.UploadImageAsync(_user.Id);
            _profileImage = pickedFileModel.ImageBytes;
            OnUploadImageFinally();
        }
        public delegate void UploadImageEventHandler();
        public event UploadImageEventHandler UploadImageFinally;

        protected virtual void OnUploadImageFinally()
        {
            UploadImageFinally?.Invoke();
        }

        public override void Prepare(UserModel parameter)
        {
            _user = parameter;
        }

        #region Validation
        public void CheckEmail()
        {
            _emailValid = false;
            if (string.IsNullOrEmpty(_email))
            {
                AlertMessage = "Enter e-mail address!";
                AlertColor = UIColor.Red;
                _emailValid = false;
                return;
            }
            if (!Regex.Match(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                AlertColor = UIColor.Red;
                AlertMessage = "E-mail address is invalid!";
                _emailValid = false;
                return;
            }
            _emailValid = true;
        }



        public override void Validate()
        {
            var user = _dataService.GetItem<UserModel>(_user.Id);
            var email = _user.Email;
            if (!string.IsNullOrEmpty(_email.ToLower()) && _emailValid && (_email.ToLower() != _user.Email))
            {
                user.Email = this.Email.ToLower();
                _user.Email = this.Email;
                _dataService.SaveItem<UserModel>(user);
                AlertColor = UIColor.Green;
                AlertMessage = "All changes were successfully saved!";
                return;
            }
            if (!string.IsNullOrEmpty(_email.ToLower()) && _emailValid && (_email.ToLower() == _user.Email))
            {
                AlertColor = UIColor.Green;
                ChangesResult = true;
                AlertMessage = "All changes were successfully saved!";
                return;
            }
            Email = email;           
        }
        #endregion     
    }

    public delegate bool ChangePasswordResult();
}
