namespace TodoListManager.Core
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
