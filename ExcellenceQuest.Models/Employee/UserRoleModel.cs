namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System.Data;

    public class UserRoleModel :BaseObject
    {
        public long EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual RoleModel Role { get; set; }
    }
}
