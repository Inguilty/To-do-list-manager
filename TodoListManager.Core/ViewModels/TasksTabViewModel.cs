using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListManager.Core.Enums;
using TodoListManager.Core.Models;
using TodoListManager.Core.Services;
using UIKit;
using TaskStatus = TodoListManager.Core.Enums.TaskStatus;

namespace TodoListManager.Core.ViewModels
{
    public class TasksTabViewModel : BaseViewModel
    {
        public TasksTabViewModel(IMvxNavigationService navigationService, ICommand addCommand, IDbService dbService)
            : base(navigationService)
        {
            Title = "Tasks";
            AddCommand = addCommand;
            Tasks = new MvxObservableCollection<TaskModel>();
            _dataService = dbService;
            _taskService = new TaskService(dbService);
        }

        #region Fields and Commands
        public ICommand AddCommand { get; }
        private MvxObservableCollection<TaskModel> _tasksCollection;
        private readonly IDbService _dataService;
        private TaskModel _task;
        private readonly TaskService _taskService;

        private void AddTask()
        {
            NavigationService.Navigate<CreateTaskTabViewModel>();
        }

        //public ICommand EditTaskCommand => new MvxCommand<TaskModel,int>(EditTask);
        //public ICommand ChangeTaskStatusCommand => new MvxCommand<TaskModel>(ChangeTaskStatus);
        //public ICommand DeleteTaskCommand => new MvxCommand<TaskModel>(DeleteTask);

        public IMvxNavigationService NavigationServiceProp => NavigationService;
        public MvxObservableCollection<TaskModel> Tasks
        {
            get => _tasksCollection;
            set
            {
                _tasksCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public void EditTaskCommand(TaskModel cellData, int id)
        {
            NavigationService.Navigate<EditTaskTabViewModel, TaskModel,IDbService>(_task);
            ReloadCell(id, _task.Id);
        }
        public void ChangeTaskStatusCommand(TaskModel cellData, int id)
        {
            _taskService.Update(cellData);
            ReloadCell(id, _task.Id);
        }
        public void DeleteTaskCommand(TaskModel cellData, int id)
        {
            _taskService.Delete(cellData);
            ReloadTable();
        }

        private void ReloadCell(int idCell, int taskId)
        {
            Tasks[idCell] = _dataService.GetItem<TaskModel>(taskId);
        }

        private void ReloadTable()
        {
            Tasks = new MvxObservableCollection<TaskModel>();
            var taskList = _taskService.GetUserTasks(HomeViewModel.UserModel);
            foreach (var el in taskList.ToList())
            {
                if (el.Deadline < DateTime.Now)
                {
                    el.Status = TaskStatus.Done;
                }

                if (el.Deadline >= DateTime.Now)
                {
                    if (el.Status == TaskStatus.InProcess)
                        el.Status = TaskStatus.InProcess;
                    if (el.Status == TaskStatus.NotDone)
                        el.Status = TaskStatus.NotDone;
                }
                Tasks.Add(el);
            }
        }

        public override async Task Initialize()
        {
            var taskList = _taskService.GetUserTasks(HomeViewModel.UserModel);
            foreach (var el in taskList.ToList())
            {
                if (el.Deadline < DateTime.Now)
                {
                    el.Status = TaskStatus.Done;
                    _dataService.SaveItem(el);
                }
                else
                if (el.Deadline >= DateTime.Now)
                {
                    if (el.Status == TaskStatus.InProcess)
                        el.Status = TaskStatus.InProcess;
                    if (el.Status == TaskStatus.NotDone)
                        el.Status = TaskStatus.NotDone;
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
}