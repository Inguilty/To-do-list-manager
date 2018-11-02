using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;

namespace TodoListManager.Core.ViewModels
{
    public class TasksTabViewModel : BaseViewModel<UserModel>
    {
        public TasksTabViewModel(IMvxNavigationService navigationService, IDbService dbService, UserModel user)
            : base(navigationService)
        {
            Title = "Tasks";
            Tasks = new MvxObservableCollection<TaskModel>();
            _user = user;
            _dataService = dbService;
            _taskService = new TaskService(dbService);
        }

        public MvxObservableCollection<TaskModel> Tasks
        {
            get => _tasksCollection;
            set
            {
                _tasksCollection = value;
                RaisePropertyChanged();
            }
        }

        #region Fields  
        private MvxObservableCollection<TaskModel> _tasksCollection;
        private CellContent _cellAndUser;
        private readonly IDbService _dataService;
        private TaskModel _task;
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
        public void ChangeTaskStatusCommand(TaskModel cellData, int id)
        {
            _taskService.Update(cellData);
            ReloadCell(id, _task.Id);
        }
        public async Task DeleteTaskCommand(TaskModel cellData, int id)
        {
            _taskService.Delete(cellData);
            await ReloadTable();
        }
        #endregion

        private void ReloadCell(int idCell, int taskId)
        {
            Tasks[idCell] = _dataService.GetItem<TaskModel>(taskId);
        }

        private async Task ReloadTable()
        {
            Tasks = new MvxObservableCollection<TaskModel>();
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

                Tasks.Add(el);
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

                Tasks.Add(el);
            }
            await base.Initialize();
        }

        public void CurrentTask(TaskModel model)
        {
            _task = model;
        }
    }

    public class CellContent
    {
        public UserModel User;
        public TaskModel Task;

        public CellContent(UserModel user, TaskModel task)
        {
            User = user;
            Task = task;
        }
    }
}