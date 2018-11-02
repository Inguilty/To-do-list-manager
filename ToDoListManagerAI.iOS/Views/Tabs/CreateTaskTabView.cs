using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.Enums;
using TodoListManager.Core.ViewModels;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    public partial class CreateTaskTabView : MvxViewController<CreateTaskTabViewModel>
    {
        private UIButton _saveButton;
        public CreateTaskTabView() : base(nameof(CreateTaskTabView), null)
        {
            _saveButton = new UIButton();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeView();
            dpDeadline.MinimumDate = DateTime.Now.ToNSDate();
            var set = this.CreateBindingSet<CreateTaskTabView, CreateTaskTabViewModel>();
            set.Bind(txtTitle).To(vm => vm.TaskTitle);
            set.Bind(txtDescription).To(vm => vm.Description);
            set.Bind(swtchStatus).For(l => l.SelectedSegment).To(vm => vm.TskStatus);
            set.Bind(dpDeadline).To(vm => vm.Deadline);
            set.Bind(_saveButton).To(vm => vm.SaveTask);
            set.Bind(tbxChooseDeadline).To(vm => vm.Deadline);
            set.Apply();

            btnEditDeadline.TouchDown += (args, e) =>
            {
                if (dpDeadline.Hidden)
                {
                    dpDeadline.Hidden = false;
                }
                else
                {
                    dpDeadline.Hidden = true;
                }
            };
            dpDeadline.EditingDidEnd += (args, e) => { ViewModel.Deadline = (DateTime)dpDeadline.Date; };
        }

        private void InitializeView()
        {
            Title = ViewModel.Title;
            swtchStatus.RemoveSegmentAtIndex((byte)TaskStatus.Done, false);
            dpDeadline.Hidden = true;
            tbxChooseDeadline.Enabled = false;
            btnEditDeadline.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            btnEditDeadline.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            btnEditDeadline.SetImage(new UIImage("date.png"), UIControlState.Normal);

            _saveButton = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            _saveButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            _saveButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            _saveButton.SetImage(new UIImage("save-file-option.png"), UIControlState.Normal);

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]
            {
                new UIBarButtonItem(_saveButton)
            };
        }
    }
}