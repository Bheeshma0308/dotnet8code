

namespace ExcellenceQuest.Business.Implementation
{
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    public class UserService : IUserService
    {
        #region Manage Constructor Injection 
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;
        #endregion
        #region User Service Methods
        public async Task<UserRoleModel> GetEmployeeRole(int employeeId)
        {
            return await _userRepository.GetEmployeeRole(employeeId);
        }
        public async Task<UserModel> SearchUser(string userEmail, string password)
        {
            return await _userRepository.SearchUser(userEmail, password);
        }

        #endregion
    }
}
