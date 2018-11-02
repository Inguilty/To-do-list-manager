using System.Threading.Tasks;
using CoreGraphics;
using CoreImage;
using TodoListManager.Core.Services;
using UIKit;

namespace ToDoListManagerAI.iOS.Services
{
    public class ProgressDialogHandle : IDialogHandle
    {
        private readonly string _title;
        private readonly string _message;
        public UIAlertController AlertController { get; private set; }

        public void BeginAnimation()
        {
            AlertController = UIAlertController.Create(_title, _message + "\n\n\n\n", UIAlertControllerStyle.Alert);
            var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
            {
                Center = new CGPoint(130.5, 120),
                Color = new UIColor(CIColor.WhiteColor)
            };
            activityIndicator.StartAnimating();

            AlertController.View.AddSubview(activityIndicator);

            var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            viewController.PresentViewController(AlertController, true, null);
        }
        public ProgressDialogHandle(string title, string message)
        {
            _title = title;
            _message = message;
        }
        public async Task Close()
        {
            AlertController.DismissViewController(true, null);
        }
    }
}