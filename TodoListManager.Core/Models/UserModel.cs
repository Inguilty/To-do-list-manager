using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TodoListManager.Core.Enums;

namespace TodoListManager.Core.Models
{
    [Table("Users")]
    public class UserModel : BaseEntity
    {
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        [NotNull, Unique]
        public string Login { get; set; }
        [NotNull]
        public string Password { get; set; }
        public UserType userType { get; set; }
        [NotNull, Unique]
        public string Email { get; set; }
        public byte [] photo { get; set; }
    }
}
