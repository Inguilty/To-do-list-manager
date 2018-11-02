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
    [Register ("TasksTabView")]
    partial class TasksTabView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tasksTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tasksTable != null) {
                tasksTable.Dispose ();
                tasksTable = null;
            }
        }
    }
}