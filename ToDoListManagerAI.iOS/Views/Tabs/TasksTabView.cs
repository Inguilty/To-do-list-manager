
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using TodoListManager.Core.Models;
using TodoListManager.Core.ViewModels;
using ToDoListManagerAI.iOS.Views.Cells;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    public partial class TasksTabView : MvxViewController<TasksTabViewModel>
    {
        public TasksTabView() : base(nameof(TasksTabView), null)
        {
            _addButton = new UIButton();
        }

        private readonly UIButton _addButton;

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        private async void AsyncInitialize()
        {
            await ViewModel.Initialize();
        }

        public override void ViewDidLoad()
        {
            AsyncInitialize();
            base.ViewDidLoad();
            //TabBarController.NavigationItem.LeftBarButtonItems = new UIBarButtonItem[] { TabBarController.NavigationItem.RightBarButtonItems[0], new UIBarButtonItem(_addButton) };
            _addButton.TouchDown += delegate
            {
                ViewModel.AddCommand.Execute(ViewModel.NavigationServiceProp);
            };
            _addButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _addButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            _addButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            _addButton.SetImage(new UIImage("add.png"), UIControlState.Normal);

            var source = new MySimpleTableViewSource(tasksTable, TasksTableViewCell.Key, TasksTableViewCell.Key);

            var set = this.CreateBindingSet<TasksTabView, TasksTabViewModel>();
            set.Bind(source).To(vm => vm.Tasks);
            set.Apply();

            tasksTable.Source = source;
            tasksTable.ContentInset = UIEdgeInsets.FromString("20.0, 20.0, 20.0, 20.0");
            tasksTable.RowHeight = 100;
            tasksTable.ReloadData();

            source.SelectedItemChanged += (args, e) =>
            {
                NSIndexPath indexPath = tasksTable.IndexPathForSelectedRow;
                int index = indexPath.Row;
                ActionSheetButtonsTouchUpInside(args, e, index);
                ViewModel.CurrentTask((TaskModel)source.SelectedItem);
            };
        }


        protected void ActionSheetButtonsTouchUpInside(object sender, EventArgs e, int index)
        {
            UIActionSheet actionSheet = new UIActionSheet("Actions");
            actionSheet.AddButton("Edit");
            actionSheet.AddButton("Change status");
            actionSheet.AddButton("Delete");
            actionSheet.AddButton("Cancel");
            actionSheet.DestructiveButtonIndex = 0;
            actionSheet.CancelButtonIndex = 3;

            var source = sender as MvxSimpleTableViewSource;
            var currCell = source.SelectedItem as TaskModel;

            actionSheet.Clicked += delegate (object a, UIButtonEventArgs but)
            {
                if (but.ButtonIndex == 0)
                {
                    ViewModel.EditTaskCommand(currCell, index);
                    ReloadRows();
                }
                if (but.ButtonIndex == 1)
                {
                    ViewModel.ChangeTaskStatusCommand(currCell, index);
                    ReloadRows();
                }
                if (but.ButtonIndex == 2)
                {
                    ViewModel.DeleteTaskCommand(currCell, index);
                    ReloadRows();
                }

            };

            actionSheet.ShowInView(View);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TabBarController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(_addButton);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            TabBarController.NavigationItem.LeftBarButtonItem = null;
        }
        private void ReloadRows()
        {
            tasksTable.BeginUpdates();
            tasksTable.ReloadRows(tasksTable.IndexPathsForVisibleRows, UITableViewRowAnimation.None);
            tasksTable.EndUpdates();
        }
    }

    public class MySimpleTableViewSource : MvxSimpleTableViewSource
    {
        public MySimpleTableViewSource(UITableView table, NSString firstKey, NSString secondKey)
            : base(table, firstKey, secondKey)
        {

        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            var tasksData = GetItemAt(indexPath);
            TaskModel oldModel = (TaskModel)tasksData;
            var model = new TaskItem()
            {
                Status = oldModel.Status,
                Deadline = oldModel.Deadline.ToString("MM/dd/yyyy hh:mm tt")
            };
            if (model.Status == TodoListManager.Core.Enums.TaskStatus.NotDone)
                model.StatusColor = UIColor.Red;
            else if (model.Status == TodoListManager.Core.Enums.TaskStatus.InProcess)
                model.StatusColor = UIColor.Blue;
            else if (model.Status == TodoListManager.Core.Enums.TaskStatus.Done)
                model.StatusColor = UIColor.Green;

            ((TasksTableViewCell)cell).SetStatusColorCommand.Execute(model.StatusColor);
            ((TasksTableViewCell)cell).SetDeadlineCommand.Execute(model.Deadline);
        }

    }
}