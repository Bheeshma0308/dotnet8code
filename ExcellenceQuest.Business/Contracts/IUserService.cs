using ExcellenceQuest.Models.Employee;
using System.Threading.Tasks;

namespace ExcellenceQuest.Business.Contracts
{
    public interface IUserService
    {
       // Task<UserModel> SearchUserExactMatch(string userEmail);
        Task<UserRoleModel> GetEmployeeRole(int employeeId);
        Task<UserModel> SearchUser(string userEmail, string password);
     }
}
