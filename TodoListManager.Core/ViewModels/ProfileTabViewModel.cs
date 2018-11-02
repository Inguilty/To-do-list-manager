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
    public class ProfileTabViewModel : BaseViewModel
    {
        public ProfileTabViewModel(IMvxNavigationService navigationService, IFilePickerService service, IDbService dataService)
            : base(navigationService)
        {
            Title = "Profile";
            _dataService = dataService;
            _filePicker = service;
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
            _firstName = HomeViewModel.UserModel.FirstName;
            _lastName = HomeViewModel.UserModel.LastName;
            _profileImage = HomeViewModel.UserModel.photo;
            _email = HomeViewModel.UserModel.Email.ToLower();
        }

        public ICommand SaveCommand => new MvxCommand(Validate);
        public ICommand ChangePasswordCommand => new MvxCommand(ChangePassword);

        public event ChangePasswordResult Result;

        public void Begin()
        {
            if (Result != null && Result.Invoke())
            {
                UIAlertView alert = new UIAlertView()
                {
                    Title = "About",
                    Message = "Password has been successfully changed!"
                };
                alert.AddButton("OK");
                alert.Show();
            }
        }
        private void ChangePassword()
        {
            var cls = this;
            NavigationService.Navigate<EditPasswordTabViewModel, IDbService/*,ProfileTabViewModel*/>(_dataService);
        }

        public ICommand UploadImageCommand => new MvxAsyncCommand(UploadImageAsync);

        private async Task UploadImageAsync()
        {
            var pickedFileModel = await _filePicker.UploadImageAsync(HomeViewModel.UserModel.Id);
            _profileImage = pickedFileModel.ImageBytes;
            OnUploadImageFinally();
        }
        public delegate void UploadImageEventHandler();
        public event UploadImageEventHandler UploadImageFinally;

        protected virtual void OnUploadImageFinally()
        {
            UploadImageFinally?.Invoke();
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
            }
            else if (!Regex.Match(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                AlertColor = UIColor.Red;
                AlertMessage = "E-mail address is invalid!";
                _emailValid = false;
            }
            else
                _emailValid = true;
        }



        public override void Validate()
        {
            var user = _dataService.GetItem<UserModel>(HomeViewModel.UserModel.Id);
            var email = HomeViewModel.UserModel.Email;
            if (!string.IsNullOrEmpty(_email.ToLower()) && _emailValid && (_email.ToLower() != HomeViewModel.UserModel.Email))
            {
                user.Email = this.Email.ToLower();
                HomeViewModel.UserModel.Email = this.Email;
                _dataService.SaveItem<UserModel>(user);
                AlertColor = UIColor.Green;
                AlertMessage = "All changes were successfully saved!";
            }
            else if (!string.IsNullOrEmpty(_email.ToLower()) && _emailValid && (_email.ToLower() == HomeViewModel.UserModel.Email))
            {
                AlertColor = UIColor.Green;
                ChangesResult = true;
                AlertMessage = "All changes were successfully saved!";
            }
            else
            {
                Email = email;
            }
        }
        #endregion      
    }

    public delegate bool ChangePasswordResult();
}
