using System;
using System.Collections.Generic;
using TodoListManager.Core.Models;
using TodoListManager.Core.ViewModels;

namespace TodoListManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IDbService _dataService;

        public TaskService(IDbService dataService)
        {
            _dataService = dataService;
        }

        public void Update(CurrentTaskItem task)
        {
            var item = _dataService.GetItem<TaskModel>(task.UserId);

            switch (item.Status)
            {
                case (Enums.TaskStatus.NotDone): item.Status = Enums.TaskStatus.InProcess; break;
                case (Enums.TaskStatus.InProcess): item.Status = Enums.TaskStatus.Done; item.Deadline = DateTime.Now; break;
                case (Enums.TaskStatus.Done): item.Status = Enums.TaskStatus.Done; break;;
            }
            _dataService.SaveItem(item);
        }

        public void Delete(CurrentTaskItem task)
        {
            _dataService.DeleteItem<TaskModel>(task.UserId);
        }

        public IEnumerable<TaskModel> GetUserTasks(UserModel user)
        {
            return  _dataService.Query<TaskModel>("SELECT * FROM Tasks WHERE UserId = ? ", user.Id);
        }
    }
}