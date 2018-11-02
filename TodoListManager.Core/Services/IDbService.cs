using MvvmCross;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public interface IDbService
    {
        void CreateTable<T>();
        IEnumerable<T> GetAllItems<T>() where T : new();
        T GetItem<T>(int id) where T : new();
        int DeleteItem<T>(int id) where T : BaseEntity;
        int SaveItem<T>(T item) where T : BaseEntity;
        
        IEnumerable<T> Query<T>(string query, params object[] args) where T : new();
    }
}
