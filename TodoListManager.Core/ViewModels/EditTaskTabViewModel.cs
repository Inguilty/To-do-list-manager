using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Windows.Input;
using Foundation;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class EditTaskTabViewModel : BaseViewModel <CellContent>
    {
        public EditTaskTabViewModel(IMvxNavigationService service, IDbService dataService)
        : base(service)
        {
            _service = service;
            _dataService = dataService;
        }

        #region Fields and Properties
        private UserModel _user;
        private string _taskTitle;
        private string _description;
        private DateTime _deadline;
        private byte _status;
        private readonly IMvxNavigationService _service;
        private readonly IDbService _dataService;

        private TaskModel _currentTask;
        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                RaisePropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }
        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                RaisePropertyChanged();
            }
        }
        public byte TskStatus
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand SaveCommand => new MvxCommand(Save);
        public ICommand DeleteCommand => new MvxCommand(DeleteTask);
        public ICommand ExtendDeadlineCommand => new MvxCommand<NSDate>(ExtendDeadline);

        private void ExtendDeadline(NSDate deadlineDate)
        {
            var deadline = (DateTime)deadlineDate;
            var currentDate = DateTime.Now;

            if (deadline <= currentDate)
            {
                TskStatus = 2;
            }
            if (deadline > currentDate)
            {
                TskStatus = 1;
            }
            Deadline = deadline;
        }

        private void Save()
        {
            var stat = Enums.TaskStatus.NotDone;
            if (_status == 0)
                stat = Enums.TaskStatus.NotDone;
            else if (_status == 1)
                stat = Enums.TaskStatus.InProcess;
            else if (_status == 2)
                stat = Enums.TaskStatus.Done;

            var item = _dataService.GetItem<TaskModel>(_currentTask.Id);

            item.Title = TaskTitle;
            item.Description = this.Description;
            item.Deadline = this.Deadline;
            item.Status = stat;

            _dataService.SaveItem<TaskModel>(item);
            ViewDispose(this);
        }

        private void DeleteTask()
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = "Alert",
                Message = "Are you sure want to delete current task?"
            };
            alert.AddButton("Yes");
            alert.AddButton("No");
            alert.Show();
            alert.Clicked += (e, args) =>
            {
                if (args.ButtonIndex == 0)
                {
                    _dataService.DeleteItem<TaskModel>(_currentTask.Id);
                    ViewDispose(this);
                }
            };
        }

        public override void Prepare(CellContent parameter)
        {
            Title = parameter.Task.Title;

            _user = parameter.User;
            _currentTask = parameter.Task;

            byte stat = 0;
            if (parameter.Task.Status == Enums.TaskStatus.NotDone)
                stat = 0;
            else if (parameter.Task.Status == Enums.TaskStatus.InProcess)
                stat = 1;
            else if (parameter.Task.Status == Enums.TaskStatus.Done)
                stat = 2;

            TaskTitle = parameter.Task.Title;
            Description = parameter.Task.Description;
            TskStatus = stat;
            Deadline = parameter.Task.Deadline;
            base.Prepare();
        }
    }
}
