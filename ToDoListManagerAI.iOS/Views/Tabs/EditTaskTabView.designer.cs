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
    [Register ("EditTaskTabView")]
    partial class EditTaskTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnEditDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker datePckrDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl swtchStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxChooseDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxTask { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnEditDeadline != null) {
                btnEditDeadline.Dispose ();
                btnEditDeadline = null;
            }

            if (datePckrDeadline != null) {
                datePckrDeadline.Dispose ();
                datePckrDeadline = null;
            }

            if (lblDeadline != null) {
                lblDeadline.Dispose ();
                lblDeadline = null;
            }

            if (swtchStatus != null) {
                swtchStatus.Dispose ();
                swtchStatus = null;
            }

            if (tbxChooseDeadline != null) {
                tbxChooseDeadline.Dispose ();
                tbxChooseDeadline = null;
            }

            if (tbxTask != null) {
                tbxTask.Dispose ();
                tbxTask = null;
            }

            if (tbxTitle != null) {
                tbxTitle.Dispose ();
                tbxTitle = null;
            }
        }
    }
}