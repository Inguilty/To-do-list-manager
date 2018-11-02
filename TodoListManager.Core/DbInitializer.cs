using TodoListManager.Core.Models;
using TodoListManager.Core.Services;

namespace TodoListManager.Core
{
    public class DbInitializer
    {
        private readonly IDbService _dbService;

        public DbInitializer(IDbService service)
        {
            _dbService = service;
        }

        public void Seed()
        {
            _dbService.CreateTable<UserModel>();
            _dbService.CreateTable<TaskModel>();
        }
    }
}
