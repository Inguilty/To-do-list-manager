using System;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using TodoListManager.Core.ViewModels;
using TodoListManager.Core.Services;
using ToDoListManagerAI.iOS.Views.Tabs;
using UIKit;
using MvvmCross.Navigation;

namespace ToDoListManagerAI.iOS.Views
{
    public partial class HomeView : MvxTabBarViewController<HomeViewModel>
    {
        private readonly bool _load = false;

        public HomeView()
        {
            ViewControllerSelected += (sender, args) =>
            {
                Title = SelectedViewController.Title;
            };

            _load = true;
            ViewDidLoad();
        }

        public sealed override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (!_load)
                return;

            var settingsButton = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            settingsButton.WidthAnchor.ConstraintEqualTo(32.0f).Active = true;
            settingsButton.HeightAnchor.ConstraintEqualTo(32.0f).Active = true;
            settingsButton.SetImage(new UIImage("menu.png"), UIControlState.Normal);
            settingsButton.TouchDown += ActionSheetButtonsTouchUpInside;
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(settingsButton);

            var filePickerService = Mvx.IoCProvider.Resolve<IFilePickerService>();
            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            var rssService = Mvx.IoCProvider.Resolve<IJsonService>();
            var dbService = Mvx.IoCProvider.Resolve<IDbService>();

            var tabBarViewControllers = new[]
            {                
                CreateTableViewController<TasksTabView, TasksTabViewModel>("Tasks","paper-plane.png",0 ,navigationService, dbService, ViewModel.User),
                CreateTableViewController<NewsTabView, NewsTabViewModel>("News", "newspaper.png",1,navigationService,rssService),
                CreateTableViewController<ProfileTabView,ProfileTabViewModel>("Profile","avatar.png",2, navigationService, filePickerService, dbService, ViewModel.User),
            };

            ViewControllers = tabBarViewControllers;
            SelectedViewController = tabBarViewControllers[0];
        }

        private void ActionSheetButtonsTouchUpInside(object sender, EventArgs e)
        {
            UIActionSheet actionSheet = new UIActionSheet("Actions");
            actionSheet.AddButton("About");
            actionSheet.AddButton("Settings");
            actionSheet.AddButton("Log out");
            actionSheet.AddButton("Cancel");
            actionSheet.DestructiveButtonIndex = 2;
            actionSheet.CancelButtonIndex = 3;

            actionSheet.Clicked += delegate (object a, UIButtonEventArgs but) {
                if (but.ButtonIndex == 0)
                {
                    ViewModel.About();
                }
                if (but.ButtonIndex == 1)
                {
                    ViewModel.OpenSettings();
                }
                if (but.ButtonIndex == 2)
                {
                    ViewModel.LogOutCommand.Execute(null);
                }
            };

            actionSheet.ShowInView(View);
        }

        private UIViewController CreateTableViewController<TCtrl, TViewModel>(string name, string icon, nint index, params object[] services)
             where TCtrl : MvxViewController
            where TViewModel : MvxViewModel
        {
            if (Activator.CreateInstance(typeof(TCtrl)) is TCtrl viewCtrl)
            {
                viewCtrl.Title = name;
                viewCtrl.ViewModel = services != null && services.Length > 0 ?
                    Activator.CreateInstance(typeof(TViewModel), services) as TViewModel :
                    Activator.CreateInstance(typeof(TViewModel)) as TViewModel;

                viewCtrl.TabBarItem = new UITabBarItem(name, UIImage.FromFile(icon), index);
                return viewCtrl;
            }

            return null;
        }
    }
}