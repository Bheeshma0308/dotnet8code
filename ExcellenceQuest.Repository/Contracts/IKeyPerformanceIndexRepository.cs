namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IKeyPerformanceIndexRepository :  IGenericRepository<KeyPerformanceIndexModel>
    {
        // Task<KeyPerformanceIndex> Save(KeyPerformanceIndexModel obj);
        Task<List<TopScorerModel>> GetTopAchieversByKpiId(int kpiId);
        Task<List<TopScorerModel>> GetTopAchieversBySubCompetencyId(int subCompetencyId);
        Task<List<TopScorerModel>> GetTopAchieversBySubCompetencies();
        Task<List<TopScorerModel>> GetTopAchieversForAllKpis();
        Task<List<TopScorerModel>> GetTopAchievers();
    }
}
