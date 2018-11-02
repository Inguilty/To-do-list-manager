using CoreAnimation;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.ViewModels;
using UIKit;

namespace ToDoListManagerAI.iOS.Views
{
    public partial class RegistrationView : MvxViewController<RegistrationViewModel>
    {
        public RegistrationView() : base(nameof(RegistrationView), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeView();

            var set = this.CreateBindingSet<RegistrationView, RegistrationViewModel>();
            if (ViewModel.ProfilePicture != null)
            {
                var data = NSData.FromArray(ViewModel.ProfilePicture);
                imgViewAvatar.Image = UIImage.LoadFromData(data);                
            }
            set.Bind(tbxEmail).To(vm => vm.Email);
            set.Bind(tbxFirstName).To(vm => vm.FirstName);
            set.Bind(tbxLastName).To(vm => vm.LastName);
            set.Bind(tbxLogin).To(vm => vm.Login);
            set.Bind(tbxPassword).To(vm => vm.Password);
            set.Bind(lblInfoErrors).To(vm => vm.AlertMessage);
            set.Bind(btnChooseFile).To(vm => vm.ChooseFileCommand);
            set.Apply();

            btnSubmit.TouchDown += (sender, args) =>
            {
                ViewModel.Validate(); 
                ViewModel.SubmitCommand.Execute(null);
            };
            tbxLogin.EditingDidEnd += (sender, args) => { ViewModel.CheckLogin(); };
            tbxEmail.EditingDidEnd += (sender, args) => { ViewModel.CheckEmail(); };
            tbxPassword.EditingDidEnd += (sender, args) =>
            {
                ViewModel.IsPasswordLengthValid();
            };
            ViewModel.UploadImageFinally += UpdateProfileImage;
        }

        private void InitializeView()
        {
            Title = ViewModel.Title;
            tbxEmail.ResignFirstResponder();
            tbxPassword.ResignFirstResponder();
            CALayer profileImageCircle = imgViewAvatar.Layer;
            profileImageCircle.CornerRadius = 75;
            profileImageCircle.MasksToBounds = true;
            imgViewAvatar.Image = new UIImage("ProfileAvatar.png");
        }

        private void UpdateProfileImage()
        {
            if (ViewModel.ProfilePicture != null)
            {
                var data = NSData.FromArray(ViewModel.ProfilePicture);
                imgViewAvatar.Image = UIImage.LoadFromData(data);
            }
        }
    }
}