using SQLite;
using System;
using TodoListManager.Core.Enums;

namespace TodoListManager.Core.Models
{
    [Table("Tasks")]
    public class TaskModel: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
