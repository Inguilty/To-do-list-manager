using System;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using TodoListManager.Core.Services;
using TodoListManager.Core.ViewModels;

namespace TodoListManager.Core
{
    public class App : MvxApplication
    {
        //private static IDbService _repo;
        //public static IDbService DataService => _repo ?? (_repo = new DbService());

        public const string DATABASE_NAME = "UsersDatabase.db";

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            DbInitializer dbService = new DbInitializer(new DbService());
            dbService.Seed();

            RegisterAppStart<LoginViewModel>();
        }
    }
}
