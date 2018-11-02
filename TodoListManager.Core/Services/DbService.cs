using System.Collections.Generic;
using System.Linq;
using MvvmCross;
using SQLite;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public class DbService : IDbService
    {
        private readonly SQLiteConnection _database;

        public DbService()
        {
            var iSQlite = Mvx.IoCProvider.Resolve<ISQLite>();
            var databasePath = iSQlite.GetDatabasePath(App.DatabaseName);
            _database = new SQLiteConnection(databasePath);
        }

        public void CreateTable<T>()
        {
            _database.CreateTable<T>();
        }

        public IEnumerable<T> GetAllItems<T>() where T : new()
        {
            return (from i in _database.Table<T>() select i).ToList();
        }

        public T GetItem<T>(int id) where T : new()
        {
            return _database.Get<T>(id);
        }

        public int DeleteItem<T>(int id) where T : BaseEntity
        {
            return _database.Delete<T>(id);
        }

        public int SaveItem<T>(T item) where T : BaseEntity
        {
            return item.Id != 0 ? _database.Update(item) : _database.Insert(item);
        }

        public IEnumerable<T> Query<T>(string query, params object[] args) where T : new()
        {
            var result = _database.Query<T>(query, args);
            return result;
        }
    }
}
