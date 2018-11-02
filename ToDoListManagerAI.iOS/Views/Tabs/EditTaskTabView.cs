using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.Enums;
using TodoListManager.Core.ViewModels;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    public partial class EditTaskTabView : MvxViewController<EditTaskTabViewModel>
    {
        private UIButton _deleteButton;
        private UIButton _saveButton;
        private UIButton _editButton;

        public EditTaskTabView() : base(nameof(EditTaskTabView), null)
        {
            _deleteButton = new UIButton();
            _saveButton = new UIButton();
            _editButton = new UIButton();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeView();

            var set = this.CreateBindingSet<EditTaskTabView, EditTaskTabViewModel>();
            set.Bind(tbxTitle).To(vm => vm.TaskTitle);
            set.Bind(tbxTask).To(vm => vm.Description);
            set.Bind(tbxChooseDeadline).To(vm => vm.Deadline);
            set.Bind(swtchStatus).For(l => l.SelectedSegment).To(vm => vm.TskStatus);
            set.Bind(datePckrDeadline).To(vm => vm.Deadline);
            set.Bind(_saveButton).To(vm => vm.SaveCommand);
            set.Bind(_deleteButton).To(vm => vm.DeleteCommand);
            set.Apply();

            swtchStatus.ValueChanged += (args, e) =>
            {
                if (swtchStatus.SelectedSegment == (byte)TaskStatus.Done)
                {
                    swtchStatus.RemoveSegmentAtIndex((byte)TaskStatus.Done, true);
                    ViewModel.Deadline = DateTime.MinValue;
                }
            };

            btnEditDeadline.TouchDown += (args, e) =>
            {
                if (datePckrDeadline.Hidden)
                {
                    datePckrDeadline.Hidden = false;
                }
                else
                {
                    datePckrDeadline.Hidden = true;
                }
            };
            datePckrDeadline.ValueChanged += (args, e) =>
            {
                if (ViewModel.TskStatus == (byte)TaskStatus.Done)
                {
                    ViewModel.ExtendDeadlineCommand.Execute(datePckrDeadline.Date);
                    swtchStatus.Enabled = true;
                    ViewModel.TskStatus = (byte)TaskStatus.InProcess;
                    swtchStatus.SelectedSegment = ViewModel.TskStatus;
                    swtchStatus.RemoveSegmentAtIndex((byte)TaskStatus.Done, true);
                }
            };

            datePckrDeadline.EditingDidEnd += (args, e) =>
            {
                ViewModel.Deadline = (DateTime)datePckrDeadline.Date;
            };

            _editButton.TouchDown += (args, e) =>
            {
                if (!tbxTitle.Enabled)
                {
                    tbxTitle.Enabled = true;
                    tbxTask.Enabled = true;
                    if (ViewModel.TskStatus != 2)
                    {
                        swtchStatus.Enabled = true;                     
                    }
                    else
                    {
                        swtchStatus.SelectedSegment = 2;
                        swtchStatus.Enabled = false;
                    }
                    btnEditDeadline.Enabled = true;
                    _deleteButton.Hidden = false;
                    _deleteButton.Enabled = true;
                    _saveButton.Hidden = false;
                    _saveButton.Enabled = true;
                }
                else
                {
                    tbxTitle.Enabled = false;
                    tbxTask.Enabled = false;
                    swtchStatus.Enabled = false;
                    btnEditDeadline.Enabled = false;
                    _deleteButton.Hidden = true;
                    _deleteButton.Enabled = false;
                    _saveButton.Hidden = true;
                    _saveButton.Enabled = false;
                }
            };
            _saveButton.TouchDown += (args, e) =>
            {
                tbxTitle.Enabled = false;
                tbxTask.Enabled = false;
                swtchStatus.Enabled = false;
                btnEditDeadline.Enabled = false;
            };
        }

        private void InitializeView()
        {
            Title = ViewModel.Title;
            swtchStatus.Enabled = false;
            if (ViewModel.TskStatus != (byte) TaskStatus.Done)
            {
                swtchStatus.RemoveSegmentAtIndex((byte)TaskStatus.Done, false);
                swtchStatus.SelectedSegment = ViewModel.TskStatus;
            }
            else
            {               
                swtchStatus.SelectedSegment = (byte)TaskStatus.Done;
            }

            datePckrDeadline.MinimumDate = DateTime.UtcNow.ToNSDate();
            datePckrDeadline.Hidden = true;
            tbxChooseDeadline.Enabled = false;
            btnEditDeadline.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            btnEditDeadline.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            btnEditDeadline.SetImage(new UIImage("date.png"), UIControlState.Normal);
            tbxTitle.Enabled = false;
            tbxTask.Enabled = false;
            btnEditDeadline.Enabled = false;

            if (_deleteButton != null && _saveButton != null && _editButton != null)
            {
                _deleteButton = new UIButton
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                _deleteButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
                _deleteButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
                _deleteButton.SetImage(new UIImage("rubbish-bin.png"), UIControlState.Normal);

                _saveButton = new UIButton
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                _saveButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
                _saveButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
                _saveButton.SetImage(new UIImage("save-file-option.png"), UIControlState.Normal);

                _editButton = new UIButton
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                _editButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
                _editButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
                _editButton.SetImage(new UIImage("editProfile.png"), UIControlState.Normal);

                NavigationItem.RightBarButtonItems = new UIBarButtonItem[]
                {
                    new UIBarButtonItem(_saveButton),
                    new UIBarButtonItem(_deleteButton),                    
                    new UIBarButtonItem(_editButton)
                };
                _deleteButton.Hidden = true;
                _deleteButton.Enabled = false;
                _saveButton.Hidden = true;
                _saveButton.Enabled = false;
            }
        }
    }
}