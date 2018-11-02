using System;
using System.IO;
using TodoListManager.Core;

namespace ToDoListManagerAI.iOS.Utils
{
    public class SQLite_iOS : ISQLite
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }
            return path;
        }
    }
}