using System.Collections.Generic;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public interface ITaskService
    {
        void Update(TaskModel data);
        void Delete(TaskModel data);

        IEnumerable<TaskModel> GetUserTasks(UserModel user);
    }
}
