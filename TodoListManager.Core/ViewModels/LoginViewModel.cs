using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using System.Linq;
using System.Threading;

namespace TodoListManager.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(IMvxNavigationService navigationService, IDbService dbService)
            : base(navigationService)
        {
            _dataService = dbService;           
        }

        #region Fields and Properties
        private readonly IDbService _dataService;
        private string _username;
        private string _password;
        private string _email;
        private bool _isValid;
        private UserModel _userModel;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
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
        #endregion

        public IMvxCommand LoginCommand => new MvxAsyncCommand(LoginAsync);
        public IMvxCommand SignUpCommand => new MvxAsyncCommand(SignUpAsync);
        public ICommand ExitCommand => new MvxCommand(Exit);

        private void Exit()
        {
            Thread.CurrentThread.Abort();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Title = "Login";
        }

        private async Task LoginAsync()
        {
            if (_isValid)
            {
                await NavigationService.Navigate<HomeViewModel, UserModel>(_userModel);
                await NavigationService.Close(this);
            }
        }
        private async Task SignUpAsync()
        {
            await NavigationService.Navigate<RegistrationViewModel>();
        }

        public override void Validate()
        {
            if (!string.IsNullOrWhiteSpace(_email.ToLower()) || !string.IsNullOrWhiteSpace(_username.ToLower()) && !string.IsNullOrWhiteSpace(_password))
            {
                var user = _dataService.Query<UserModel>("SELECT * FROM Users WHERE Login = ? OR Email = ?", _username.ToLower(), _email.ToLower()).SingleOrDefault();
                if (user == null)
                {
                    AlertMessage = "User with such login does not exist.";
                    return;
                }
                _isValid = user.Password == _password;
                _userModel = user;
                if (!_isValid)
                {
                    _userModel = null;
                    AlertMessage = "Invalid login or password!";
                    Password = "";
                }
            }
        }
    }
}
