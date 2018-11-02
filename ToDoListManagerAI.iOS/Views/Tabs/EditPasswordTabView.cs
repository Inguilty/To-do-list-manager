
using System;
using System.Drawing;
using CoreImage;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.ViewModels;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    [MvxModalPresentation]
    public partial class EditPasswordTabView : MvxViewController<EditPasswordTabViewModel>
    {
        public EditPasswordTabView() : base(nameof(EditPasswordTabView), null)
        {

        }
        
        public EditPasswordTabView(IntPtr handle) : base(handle)
        {
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Editing password";
            lblAlerts.TextColor = new UIColor(CIColor.RedColor);
            var set = this.CreateBindingSet<EditPasswordTabView, EditPasswordTabViewModel>();
            set.Bind(tbxOldPass).To(vm => vm.OldPassword);
            set.Bind(tbxNewPass).To(vm => vm.NewPassword);
            set.Bind(tbxConfNewPass).To(vm => vm.ConfirmNewPassword);
            set.Bind(btnCancel).To(vm => vm.CancelCommand);
            set.Bind(lblAlerts).To(vm => vm.AlertMessage);
            set.Bind(lblAlerts).For(l => l.TextColor).To(vm => vm.AlertColor);
            set.Apply();
            var viewTap = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
            });
            View.AddGestureRecognizer(viewTap);

            tbxOldPass.EditingDidEnd += (sender, e) => { ViewModel.CheckOldPassword(); };
            tbxNewPass.EditingDidEnd += (sender, e) => { ViewModel.IsPasswordValid(); };
            tbxConfNewPass.EditingDidEnd += (sender, e) => { ViewModel.CheckConfirmPassword(); };
            btnSavePass.TouchDown += (sender, e) => {
                if (ViewModel.IsNewPasswordValid && ViewModel.IsOldPasswordValid )
                {
                    ViewModel.Validate();
                }
                else
                {
                    ViewModel.AlertMessage = "Fields was filled incorrectly!";
                    ViewModel.AlertColor = UIColor.Red;
                }
            };
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            //ProfileTabViewModel.Begin();
        }
    }
}