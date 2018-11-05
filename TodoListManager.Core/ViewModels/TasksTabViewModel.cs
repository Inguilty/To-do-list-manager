using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;
using TaskStatus = TodoListManager.Core.Enums.TaskStatus;

namespace TodoListManager.Core.ViewModels
{
    public class TasksTabViewModel : BaseViewModel<UserModel>
    {
        public TasksTabViewModel(IMvxNavigationService navigationService, IDbService dbService, UserModel user)
            : base(navigationService)
        {
            Title = "Tasks";
            Tasks = new MvxObservableCollection<CurrentTaskItem>();
            _user = user;
            _dataService = dbService;
            _taskService = new TaskService(dbService);
        }

        public MvxObservableCollection<CurrentTaskItem> Tasks
        {
            get => _tasksCollection;
            set
            {
                _tasksCollection = value;
                RaisePropertyChanged();
            }
        }

        #region Fields  
        private MvxObservableCollection<CurrentTaskItem> _tasksCollection;
        private CellContent _cellAndUser;
        private readonly IDbService _dataService;
        private CurrentTaskItem _task;
        private readonly TaskService _taskService;
        private UserModel _user;
        #endregion

        #region Comands    

        public ICommand AddCommand => new MvxAsyncCommand(AddTask);
        public ICommand EditTaskCommand => new MvxAsyncCommand(EditTask);

        public MvxAsyncCommand ReloadCommandAsync => new MvxAsyncCommand(async () => { await ReloadTable(); });


        private async Task AddTask()
        {
            await NavigationService.Navigate<CreateTaskTabViewModel, UserModel>(_user);
            await ReloadTable();
        }
        private async Task EditTask()
        {
            _cellAndUser = new CellContent(_user, _task);
            await NavigationService.Navigate<EditTaskTabViewModel, CellContent>(_cellAndUser);
            await ReloadTable();
        }
        public void ChangeTaskStatusCommand(CurrentTaskItem cellData, int id)
        {
            _taskService.Update(cellData);
            ReloadCell(id, _task.UserId);
        }
        public async Task DeleteTaskCommand(CurrentTaskItem cellData, int id)
        {
            _taskService.Delete(cellData);
            await ReloadTable();
        }
        #endregion

        private void ReloadCell(int idCell, int taskId)
        {
            var item = _dataService.GetItem<TaskModel>(taskId);
            var newItem = new CurrentTaskItem()
            {
                Title = item.Title,
                Description = item.Description,
                Status = item.Status,
                UserId = item.Id,
                Deadline = item.Deadline.ToString("MM/dd/yyyy hh:mm tt"),
                DtDeadline = item.Deadline
            };
            Tasks[idCell] = newItem;
        }

        private async Task ReloadTable()
        {
            Tasks = new MvxObservableCollection<CurrentTaskItem>();
            var taskList = _taskService.GetUserTasks(_user);
            foreach (var el in taskList.ToList())
            {
                if (el.Deadline < DateTime.Now)
                {
                    el.Status = Enums.TaskStatus.Done;
                }

                if (el.Deadline >= DateTime.Now)
                {
                    if (el.Status == Enums.TaskStatus.InProcess)
                        el.Status = Enums.TaskStatus.InProcess;
                    if (el.Status == Enums.TaskStatus.NotDone)
                        el.Status = Enums.TaskStatus.NotDone;
                }

                var newItem = new CurrentTaskItem()
                {
                    Title = el.Title,
                    Description = el.Description,
                    Status = el.Status,
                    UserId = el.Id,
                    Deadline = el.Deadline.ToString("MM/dd/yyyy hh:mm tt"),
                    DtDeadline = el.Deadline
                };

                Tasks.Add(newItem);
            }
            await RaisePropertyChanged();
        }

        public override void Prepare(UserModel parameter)
        {
            _user = parameter;
        }

        public override async Task Initialize()
        {
            var taskList = _taskService.GetUserTasks(_user);
            var model = new CurrentTaskItem();
            foreach (var el in taskList.ToList())
            {
                if (el.Deadline < DateTime.Now)
                {
                    el.Status = Enums.TaskStatus.Done;
                    _dataService.SaveItem(el);
                }
                else
                if (el.Deadline >= DateTime.Now)
                {
                    if (el.Status == Enums.TaskStatus.InProcess)
                        el.Status = Enums.TaskStatus.InProcess;
                    if (el.Status == Enums.TaskStatus.NotDone)
                        el.Status = Enums.TaskStatus.NotDone;
                }

                model.Title = el.Title;
                model.Description = el.Description;
                model.Status = el.Status;
                model.UserId = el.UserId;
                model.Deadline = el.Deadline.ToString("MM/dd/yyyy hh:mm tt");
                model.DtDeadline = el.Deadline;

                Tasks.Add(model);
            }
            await base.Initialize();
        }

        public void CurrentTask(CurrentTaskItem model)
        {
            _task = model;
        }
    }

    public class CellContent
    {
        public UserModel User;
        public CurrentTaskItem Task;

        public CellContent(UserModel user, CurrentTaskItem task)
        {
            User = user;
            Task = task;
        }
    }

    public class CurrentTaskItem
    {     
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int UserId { get; set; }
        public string Deadline { get; set; }
        public DateTime DtDeadline { get; set; }
    }
}