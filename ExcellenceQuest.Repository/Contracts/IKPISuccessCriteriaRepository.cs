namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Models.KPI;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IKPISuccessCriteriaRepository : IGenericRepository<KPISuccessCriteriaModel>
    {
        Task<List<KPISuccessCriteriaModel>> GetKPISuccessCriteriaDetails(int GradeId);
        Task<List<KPICriteriaModel>> GetEmployeeKPICreiteria(int GradeId);
    }
}
