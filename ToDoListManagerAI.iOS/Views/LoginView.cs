using System;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using TodoListManager.Core.Services;
using TodoListManager.Core.ViewModels;

using UIKit;

namespace ToDoListManagerAI.iOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        private UIButton _infoButton;
        public LoginView() : base(nameof(LoginView), null)
        {
            _infoButton = new UIButton();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
            tbxUsername.ResignFirstResponder();
            tbxPassword.ResignFirstResponder();
            imgViewUserPicture.Image = new UIImage("ProfileAvatar.png");

            _infoButton = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _infoButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            _infoButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            _infoButton.SetImage(new UIImage("question-sign.png"), UIControlState.Normal);
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(_infoButton);


            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(tbxUsername).To(vm => vm.UserName);
            set.Bind(tbxUsername).To(vm => vm.Email);
            set.Bind(tbxPassword).To(vm => vm.Password);
            set.Bind(btnSignIn).To(vm => vm.LoginCommand);
            set.Bind(lblAlert).To(vm => vm.AlertMessage);
            set.Bind(btnSignUp).To(vm => vm.SignUpCommand);
            set.Apply();

            var viewTap = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
            });
            View.AddGestureRecognizer(viewTap);

            _infoButton.TouchDown += (sender, e) => { ViewModel.ExitCommand.Execute(null); };
            btnSignIn.TouchDown += (sender, args) => { ViewModel.Validate(); };
            tbxPassword.EditingDidEndOnExit += (sender, args) => { tbxPassword.ResignFirstResponder(); };

            var filePickerService = Mvx.IoCProvider.Resolve<IFilePickerService>();
            var dataService = Mvx.IoCProvider.Resolve<IDbService>();
            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

            AddService<RegistrationView, RegistrationViewModel>(filePickerService, dataService, navigationService);
        }
        private UIViewController AddService<TCtrl, TViewModel>(params object[] services)
            where TCtrl : MvxViewController
            where TViewModel : MvxViewModel
        {
            if (Activator.CreateInstance(typeof(TCtrl)) is TCtrl viewCtrl)
            {
                viewCtrl.ViewModel = services != null && services.Length > 0 ?
                    Activator.CreateInstance(typeof(TViewModel), services) as TViewModel :
                    Activator.CreateInstance(typeof(TViewModel)) as TViewModel;

                return viewCtrl;
            }

            return null;
        }
    }
}