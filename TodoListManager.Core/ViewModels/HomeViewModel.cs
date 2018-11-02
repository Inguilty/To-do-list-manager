using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel<UserModel>
    {
        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private readonly IMvxNavigationService _navigationService;

        public static UserModel UserModel { get; private set; }

        public override void Prepare(UserModel userModel)
        {
            UserModel = userModel;
            base.Prepare();
        }

        public ICommand LogOutCommand => new MvxCommand(LogOut);
        public void LogOut()
        {
             _navigationService.Navigate<LoginViewModel>();
             _navigationService.Close(this);
        }

        public ICommand AddCommand => new MvxCommand<IMvxNavigationService>(AddTask);
        private void AddTask(IMvxNavigationService navigationService)
        {
             _navigationService.Navigate<CreateTaskTabViewModel, IMvxNavigationService, IDbService>(navigationService);
        }

        public void About()
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = "About",
                Message = $"© copyright." +
                          $"All rights reserved."
            };
            alert.AddButton("OK");
            alert.Show();
        }

        public void OpenSettings()
        {

        }
    }
}
