// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ToDoListManagerAI.iOS.Views.Tabs
{
    [Register ("ProfileTabView")]
    partial class ProfileTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnChangePassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnChooseFile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSaveChanges { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageViewAvatar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAlerts { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView lblProfilePicture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxFirstName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxLastName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnChangePassword != null) {
                btnChangePassword.Dispose ();
                btnChangePassword = null;
            }

            if (btnChooseFile != null) {
                btnChooseFile.Dispose ();
                btnChooseFile = null;
            }

            if (btnSaveChanges != null) {
                btnSaveChanges.Dispose ();
                btnSaveChanges = null;
            }

            if (imageViewAvatar != null) {
                imageViewAvatar.Dispose ();
                imageViewAvatar = null;
            }

            if (lblAlerts != null) {
                lblAlerts.Dispose ();
                lblAlerts = null;
            }

            if (lblProfilePicture != null) {
                lblProfilePicture.Dispose ();
                lblProfilePicture = null;
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
        }
    }
}