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
    [Register ("EditPasswordTabView")]
    partial class EditPasswordTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSavePass { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAlerts { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxConfNewPass { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxNewPass { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxOldPass { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnSavePass != null) {
                btnSavePass.Dispose ();
                btnSavePass = null;
            }

            if (lblAlerts != null) {
                lblAlerts.Dispose ();
                lblAlerts = null;
            }

            if (tbxConfNewPass != null) {
                tbxConfNewPass.Dispose ();
                tbxConfNewPass = null;
            }

            if (tbxNewPass != null) {
                tbxNewPass.Dispose ();
                tbxNewPass = null;
            }

            if (tbxOldPass != null) {
                tbxOldPass.Dispose ();
                tbxOldPass = null;
            }
        }
    }
}