// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ToDoListManagerAI.iOS.Views
{
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSignIn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSignUp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgViewUserPicture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAlert { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSignIn != null) {
                btnSignIn.Dispose ();
                btnSignIn = null;
            }

            if (btnSignUp != null) {
                btnSignUp.Dispose ();
                btnSignUp = null;
            }

            if (imgViewUserPicture != null) {
                imgViewUserPicture.Dispose ();
                imgViewUserPicture = null;
            }

            if (lblAlert != null) {
                lblAlert.Dispose ();
                lblAlert = null;
            }

            if (tbxPassword != null) {
                tbxPassword.Dispose ();
                tbxPassword = null;
            }

            if (tbxUsername != null) {
                tbxUsername.Dispose ();
                tbxUsername = null;
            }
        }
    }
}