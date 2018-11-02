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
    [Register ("RegistrationView")]
    partial class RegistrationView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnChooseFile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgViewAvatar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblInfoErrors { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxFirstName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxLastName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnChooseFile != null) {
                btnChooseFile.Dispose ();
                btnChooseFile = null;
            }

            if (btnSubmit != null) {
                btnSubmit.Dispose ();
                btnSubmit = null;
            }

            if (imgViewAvatar != null) {
                imgViewAvatar.Dispose ();
                imgViewAvatar = null;
            }

            if (lblInfoErrors != null) {
                lblInfoErrors.Dispose ();
                lblInfoErrors = null;
            }

            if (tbxEmail != null) {
                tbxEmail.Dispose ();
                tbxEmail = null;
            }

            if (tbxFirstName != null) {
                tbxFirstName.Dispose ();
                tbxFirstName = null;
            }

            if (tbxLastName != null) {
                tbxLastName.Dispose ();
                tbxLastName = null;
            }

            if (tbxLogin != null) {
                tbxLogin.Dispose ();
                tbxLogin = null;
            }

            if (tbxPassword != null) {
                tbxPassword.Dispose ();
                tbxPassword = null;
            }
        }
    }
}