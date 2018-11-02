using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListManager.Core.Services
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
