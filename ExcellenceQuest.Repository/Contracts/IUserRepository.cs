
namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Models;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
       
      //  Task<UserModel> SearchUserExactMatch(string userEmail);
        Task<UserRoleModel> GetEmployeeRole(int employeeId);
        Task<UserModel> SearchUser(string userEmail, string password);

    }
}
