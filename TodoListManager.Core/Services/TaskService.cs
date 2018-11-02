using System;
using System.Collections.Generic;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IDbService _dataService;

        public TaskService(IDbService dataService)
        {
            _dataService = dataService;
        }

        public void Update(TaskModel task)
        {
            var item = _dataService.GetItem<TaskModel>(task.Id);

            switch (item.Status)
            {
                case (Enums.TaskStatus.NotDone): item.Status = Enums.TaskStatus.InProcess; break;
                case (Enums.TaskStatus.InProcess): item.Status = Enums.TaskStatus.Done; item.Deadline = DateTime.UtcNow; break;
                case (Enums.TaskStatus.Done): item.Status = Enums.TaskStatus.Done; break;;
            }
            _dataService.SaveItem(item);
        }

        public void Delete(TaskModel task)
        {
            _dataService.DeleteItem<TaskModel>(task.Id);
        }

        public IEnumerable<TaskModel> GetUserTasks(UserModel user)
        {
            return  _dataService.Query<TaskModel>("SELECT * FROM Tasks WHERE UserId = ? ", user.Id);
        }
    }
}