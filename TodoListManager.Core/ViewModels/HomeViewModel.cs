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
    public class HomeViewModel : BaseViewModel<UserModel>
    {
        public HomeViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
        }

        public UserModel User;

        public override void Prepare(UserModel userModel)
        {
            User = userModel;
            base.Prepare();
        }

        public ICommand LogOutCommand => new MvxCommand(LogOut);
        public void LogOut()
        {
            NavigationService.Navigate<LoginViewModel>();
            ViewDispose(this);
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
