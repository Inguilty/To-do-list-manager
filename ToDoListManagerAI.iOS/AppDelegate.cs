using Foundation;
using MvvmCross.Platforms.Ios.Core;
using TodoListManager.Core;
using UIKit;

namespace ToDoListManagerAI.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // UserModel Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);
            return result;
        }

        public override void OnResignActivation(UIApplication application)
        {
        }

        public override void DidEnterBackground(UIApplication application)
        {
        }

        public override void WillEnterForeground(UIApplication application)
        {
        }

        public override void OnActivated(UIApplication application)
        {
        }

        public override void WillTerminate(UIApplication application)
        {
        }
    }
}

