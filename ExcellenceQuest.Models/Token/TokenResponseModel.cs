using ExcellenceQuest.Models.Employee;

namespace ExcellenceQuest.Models.Token
{
    public class TokenResponseModel
    {
        public UserModel User { get; set; }
        public EmployeeModel Employee { get; set; }
        public UserRoleModel UserRole { get; set; }
        public string ClientName { get; set; }
        public int EmpCareerUserId { get; set; }
    }
}
