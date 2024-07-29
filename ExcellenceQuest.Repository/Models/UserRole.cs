using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public long EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
