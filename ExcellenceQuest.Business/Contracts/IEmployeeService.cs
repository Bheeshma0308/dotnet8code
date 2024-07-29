namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        Task<UserModel> GetRole(string email, string pass);

        Task<EmployeeModel> GetEmployeeDashboard(int empId);


    }
}
