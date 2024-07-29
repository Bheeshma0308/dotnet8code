
using ExcellenceQuest.Models.Common;
using System;

namespace ExcellenceQuest.Models.Employee
{
    public class UserModel : BaseObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; } 
    }

}
