using System;
using System.IO;
using TodoListManager.Core.Services;
using ToDoListManagerAI.iOS;

namespace ToDoListManagerAI.iOS.Utils
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }
            ///Users/macbookpro1/Library/Developer/CoreSimulator/Devices/FBBCFC0A-8DD8-4BC9-BBF2-D43D5B79F8CC/data/Containers/Data/Application/5AA72D6F-4648-4D4E-9449-7B7970DF52AD/Documents/../Library/UsersData
            //Users/macbookair7/Library/Developer/CoreSimulator/Devices/A86AE8FC-5B95-4524-9D35-9DE63C4467A3/data/Containers/Data/Application/802A7532-F66A-4AF3-B62C-FB5A4D6BFBFA/Documents/../Library/UsersDatabase.db
            Console.WriteLine("DBpath:{0}",path);
            return path;
        }
    }
}