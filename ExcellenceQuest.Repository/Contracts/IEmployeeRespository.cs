namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmployeeRespository 
    {
        Task<User> GetRole(string email, string pass);


        Task<EmployeeModel> GetEmployeeDashboard(int empId);
    }
}
