using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Input;
using CoreImage;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Platforms.Ios.Binding.Views;
using TodoListManager.Core.Enums;
using TodoListManager.Core.Models;
using UIKit;

namespace ToDoListManagerAI.iOS.Views.Cells
{
    public partial class TasksTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("TasksTableViewCell");
        public static readonly UINib Nib;

        static TasksTableViewCell()
        {
            Nib = UINib.FromName("TasksTableViewCell", NSBundle.MainBundle);
        }

        public ICommand SetStatusColorCommand => new MvxCommand<UIColor>(StatusColorCommand);
        public void StatusColorCommand(UIColor color)
        {
            if(color != null)
            lblStatus.TextColor = color;
        }
        public ICommand SetDeadlineCommand => new MvxCommand<string>(SetDeadline);
        public void SetDeadline(string deadline)
        {
            if (deadline != null)
                lblDeadline.Text = deadline;
        }

        protected TasksTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TasksTableViewCell, TaskModel>();
                set.Bind(lblTitle).To(vm => vm.Title);
                set.Bind(lblDeadline).To(vm => vm.Deadline);
                set.Bind(lblDescription).To(vm => vm.Description);
                set.Bind(lblStatus).To(vm => vm.Status);
                set.Apply();
            });
        }

  
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public UIColor StatusColor { get; set; }
    }
}
