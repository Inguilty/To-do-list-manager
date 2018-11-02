using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using TodoListManager.Core;
using TodoListManager.Core.Services;
using ToDoListManagerAI.iOS.Services;
using ToDoListManagerAI.iOS.Utils;
using UIKit;

namespace ToDoListManagerAI.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override IMvxApplication CreateApp()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ISQLite, SQLite_iOS>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IFilePickerService, FilePickerService>();
            return new App();
        }
    }
}