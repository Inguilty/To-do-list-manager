using MvvmCross.IoC;
using MvvmCross.ViewModels;
using TodoListManager.Core.Services;
using TodoListManager.Core.ViewModels;

namespace TodoListManager.Core
{
    public class App : MvxApplication
    {
        public const string DatabaseName = "UsersDatabase.db";

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
