using System;
using System.Collections.Generic;
using System.Text;
using TodoListManager.Core.Models;
using TodoListManager.Core.ViewModels;

namespace TodoListManager.Core.Services
{
    public interface ITaskService
    {
        void Update(TaskModel data);
        void Delete(TaskModel data);

        IEnumerable<TaskModel> GetUserTasks(UserModel user);
    }
}
