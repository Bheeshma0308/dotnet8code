namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeAchievementRepository : IGenericRepository<EmployeeAchievementModel>
    {
        Task<List<EmployeeAchievement>> GetEmployeeAchievement(int empId);
        Task<List<EmployeeKpiViewModel>> GetEmployeeKpiCount(int empId);
    }
}



