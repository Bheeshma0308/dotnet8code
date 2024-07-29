namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IKeyPerformanceIndexService
    {
        Task<List<KeyPerformanceIndexModel>> GetKPIList();
        Task<KeyPerformanceIndexModel> SaveKPI(KeyPerformanceIndexModel obj);
        Task<List<TopScorerModel>> GetTopAchieverByKpiId(int kpiId);
        Task<List<TopScorerModel>> GetTopAchieversBySubCompetencyId(int subCompetencyId);
        Task Delete(int id);
        Task<List<TopScorerModel>> GetTopAchieversBySubCompetencies();
        Task<List<TopScorerModel>> GetTopAchievers();
        Task<List<TopScorerModel>> GetTopAchieversForAllKpis();
    }
}
