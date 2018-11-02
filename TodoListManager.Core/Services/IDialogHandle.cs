using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager.Core.Services
{
    public interface IDialogHandle
    {
         Task Close();
    }
}
