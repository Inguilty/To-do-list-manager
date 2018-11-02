using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using TodoListManager.Core.Models;
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
