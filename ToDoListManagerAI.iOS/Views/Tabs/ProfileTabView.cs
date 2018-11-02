using CoreAnimation;
using CoreImage;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.ViewModels;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    public partial class ProfileTabView : MvxViewController<ProfileTabViewModel>
    {
        private UIButton _editProfile;
        public ProfileTabView() : base(nameof(ProfileTabView), null)
        {
        }

        public override void ViewDidLoad()
        {
            InitializeView();
            var set = this.CreateBindingSet<ProfileTabView, ProfileTabViewModel>();
            if (ViewModel.ProfileImage != null)
            {
                var data = NSData.FromArray(ViewModel.ProfileImage);
                imageViewAvatar.Image = UIImage.LoadFromData(data);
            }
            set.Bind(tbxFirstName).To(vm => vm.FirstName);
            set.Bind(tbxLastName).To(vm => vm.LastName);
            set.Bind(tbxEmail).To(vm => vm.Email);
            set.Bind(lblAlerts).To(vm => vm.AlertMessage);
            set.Bind(lblAlerts).For(l => l.TextColor).To(vm => vm.AlertColor);
            set.Bind(btnSaveChanges).To(vm => vm.SaveCommand);
            set.Bind(btnChooseFile).To(vm => vm.UploadImageCommand);
            set.Bind(btnChangePassword).To(vm => vm.ChangePasswordCommand);
            set.Apply();

            var viewTap = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
            });
            View.AddGestureRecognizer(viewTap);

            tbxEmail.EditingDidEnd += (sender, e) => { ViewModel.CheckEmail(); };
            tbxEmail.EditingDidBegin += (sender, e) => { btnSaveChanges.Enabled = true; };
            ViewModel.UploadImageFinally += UpdateProfileImage;
            btnSaveChanges.TouchDown += (sender, e) =>
            {
                tbxEmail.Enabled = false;
                btnSaveChanges.Enabled = false;
                btnSaveChanges.Hidden = true;
                btnChangePassword.Enabled = false;
                btnChangePassword.Hidden = true;
                btnChooseFile.Enabled = false;
                btnChooseFile.Hidden = true;
            };
            
        }

        private void InitializeView()
        {
            base.ViewDidLoad();
            tbxFirstName.Enabled = false;
            tbxLastName.Enabled = false;
            tbxEmail.Enabled = false;
            btnSaveChanges.Enabled = false;
            btnSaveChanges.Hidden = true;
            btnChangePassword.Enabled = false;
            btnChangePassword.Hidden = true;
            btnChooseFile.Enabled = false;
            btnChooseFile.Hidden = true;
            CALayer profileImageCircle = imageViewAvatar.Layer;
            profileImageCircle.CornerRadius = 70;
            profileImageCircle.MasksToBounds = true;
            imageViewAvatar.Image = new UIImage("ProfileAvatar.png");
            lblAlerts.TextColor = new UIColor(CIColor.RedColor);

            _editProfile = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _editProfile.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            _editProfile.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            _editProfile.SetImage(new UIImage("editProfile.png"), UIControlState.Normal);
            TabBarController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(_editProfile);
            _editProfile.TouchDown += delegate
            {
                if (tbxEmail.Enabled == false)
                {                  
                    tbxEmail.Enabled = true;
                    btnSaveChanges.Enabled = true;
                    btnSaveChanges.Hidden = false;
                    btnChangePassword.Enabled = true;
                    btnChangePassword.Hidden = false;
                    btnChooseFile.Enabled = true;
                    btnChooseFile.Hidden = false;
                    ViewModel.AlertMessage = "";
                }
                else
                {
                    tbxEmail.Enabled = false;
                    btnSaveChanges.Enabled = false;
                    btnSaveChanges.Hidden = true;
                    btnChangePassword.Enabled = false;
                    btnChangePassword.Hidden = true;
                    btnChooseFile.Enabled = false;
                    btnChooseFile.Hidden = true;
                }
            };
        }

        private void UpdateProfileImage()
        {
            if (ViewModel.ProfileImage != null)
            {
                var data = NSData.FromArray(ViewModel.ProfileImage);
                imageViewAvatar.Image = UIImage.LoadFromData(data);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TabBarController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(_editProfile);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            TabBarController.NavigationItem.LeftBarButtonItem = null;
        }
    }
}