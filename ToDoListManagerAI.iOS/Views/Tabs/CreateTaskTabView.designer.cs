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
    [Register ("TaskEditTabView")]
    partial class CreateTaskTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnEditDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker dpDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl swtchStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxChooseDeadline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnEditDeadline != null) {
                btnEditDeadline.Dispose ();
                btnEditDeadline = null;
            }

            if (dpDeadline != null) {
                dpDeadline.Dispose ();
                dpDeadline = null;
            }

            if (swtchStatus != null) {
                swtchStatus.Dispose ();
                swtchStatus = null;
            }

            if (tbxChooseDeadline != null) {
                tbxChooseDeadline.Dispose ();
                tbxChooseDeadline = null;
            }

            if (txtDescription != null) {
                txtDescription.Dispose ();
                txtDescription = null;
            }

            if (txtTitle != null) {
                txtTitle.Dispose ();
                txtTitle = null;
            }
        }
    }
}