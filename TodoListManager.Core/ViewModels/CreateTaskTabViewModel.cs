using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Foundation;
using TodoListManager.Core.Enums;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;

namespace TodoListManager.Core.ViewModels
{
    public class CreateTaskTabViewModel : MvxViewModel<IMvxNavigationService, IDbService>
    {
        public CreateTaskTabViewModel(IDbService dataService)
        {
            _dataService = dataService;
        }

        #region Fields and Properties
        private string _taskTitle;
        private string _description;
        private DateTime _deadline;
        private byte _status;
        private readonly IDbService _dataService;

        public IMvxNavigationService NavigationServiceProp { get; private set; }
        public string Title { get; set; }
        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                RaisePropertyChanged(() => TaskTitle);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                RaisePropertyChanged(() => Deadline);
            }
        }

        public byte TskStatus
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged(() => TskStatus);
            }
        }
        #endregion

        public ICommand SaveTask => new MvxCommand(SaveCommand);

        private void SaveCommand()
        {
            if (!string.IsNullOrEmpty(TaskTitle))
            {
                var stat = Enums.TaskStatus.NotDone;
                if (_status == 0)
                    stat = Enums.TaskStatus.NotDone;
                else if (_status == 1)
                    stat = Enums.TaskStatus.InProcess;
                else if (_status == 2)
                    stat = Enums.TaskStatus.Done;

                var model = new TaskModel()
                {
                    Title = TaskTitle,
                    Description = this.Description,
                    Deadline = this.Deadline,
                    Status = stat,
                    UserId = HomeViewModel.UserModel.Id
                };
                _dataService.SaveItem(model);
                NavigationServiceProp.Navigate<HomeViewModel>();
                NavigationServiceProp.Close(this);
            }
            else
            {
                UIAlertView alert = new UIAlertView()
                {
                    Title = "Alert",
                    Message = "You must input title!"
                };
                alert.AddButton("OK");
                alert.Show();
            }
        }
        public override void Prepare(IMvxNavigationService parameter)
        {
            NavigationServiceProp = parameter;
            base.Prepare();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Title = "Creating the task";
            Deadline = DateTime.UtcNow;
        }
    }
}
