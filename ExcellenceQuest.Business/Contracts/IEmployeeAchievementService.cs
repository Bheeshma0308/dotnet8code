namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models.Employee;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmployeeAchievementService
    {
        Task<EmployeeAchievementModel> Save(EmployeeAchievementModel model);

        Task<List<EmployeeKpiViewModel>> GetEmployeeKpiCount(int empId);
        
    }
}
