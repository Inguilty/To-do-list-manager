using System.Collections.Generic;
using TodoListManager.Core.Models;
using TodoListManager.Core.ViewModels;

namespace TodoListManager.Core.Services
{
    public interface ITaskService
    {
        void Update(/*TaskModel*/CurrentTaskItem data);
        void Delete(/*TaskModel*/CurrentTaskItem data);

        IEnumerable<TaskModel> GetUserTasks(UserModel user);
    }
}
