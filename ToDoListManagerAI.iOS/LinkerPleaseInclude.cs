using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Target;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace ToDoListManagerAI.iOS
{
    // This class is never actually executed, but when Xamarin linking is enabled it does ensure types and properties
    // are preserved in the deployed app
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public void Include(UITextField textField)
        {
            textField.Text = textField.Text + "";
            textField.EditingChanged += (sender, args) => { textField.Text = ""; };
            textField.EditingDidEnd += (sender, args) => { textField.Text = ""; };
        }

        public void Include(UIButton uiButton)
        {
            uiButton.TouchUpInside += (s, e) =>
            uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
        }

        
    }
}