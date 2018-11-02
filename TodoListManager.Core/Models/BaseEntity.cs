using SQLite;

namespace TodoListManager.Core.Models
{
    public class BaseEntity
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
    }
}
