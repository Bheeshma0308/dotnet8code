namespace ExcellenceQuest.Repository.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class KeyPerformanceIndexRepository : GenericRepository<KeyPerformanceIndex, KeyPerformanceIndexModel>, IKeyPerformanceIndexRepository
    {
        private readonly ExcellenceQuestContext _context;
        private readonly IMapper Mapper;
        public KeyPerformanceIndexRepository(ExcellenceQuestContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            Mapper = mapper;
        }

        public async Task<List<TopScorerModel>> GetTopAchieversByKpiId(int kpiId)

        {
            var employeeAchievements = await _context.EmployeeAchievements
                        .Include(ea => ea.Employee)
                        .Include(ea => ea.Kpi)
                        .Where(ea => ea.KpiId == kpiId)
                        .ToListAsync();

            var topAchieversWithKPI = employeeAchievements
                .GroupBy(ea => ea.EmployeeId)
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    AchievementDate = g.Max(e => e.AchievedOn),
                    Score = g.Sum(ea => _context.KPISuccessCriteria
                            .Where(kpisc => kpisc.KpiId == _context.KeyPerformanceIndices
                                                            .Where(kpi => kpi.Id == ea.KpiId)
                                                            .Select(kpi => kpi.Id)
                                                            .FirstOrDefault())
                            .Select(kpisc => kpisc.Weightage)
                            .FirstOrDefault()) * g.Select(ea => ea.KpiId).Distinct().Count()
                })
                .OrderByDescending(a => a.Score)
                .Select(a => new TopScorerModel
                {
                    EmployeeId = a.EmployeeId,
                    EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == a.EmployeeId)?.Name,
                    KpiId = kpiId,
                    KpiName = _context.KeyPerformanceIndices.Include(k => k.Stream)
                                .FirstOrDefault(k => k.Id == kpiId)?.Name,
                    StreamId = _context.KeyPerformanceIndices.Include(k => k.Stream)
                                        .FirstOrDefault(k => k.Id == kpiId).StreamId,
                    StreamName = _context.KeyPerformanceIndices.Include(k => k.Stream)
                                                .FirstOrDefault(k => k.Id == kpiId)?.Stream.Name,
                    GradeName =  _context.Employees.Include(k => k.Grade)
                        .FirstOrDefault(k => k.Grade.Id == k.GradeId).Grade.Name,
                    SubCompetencyName = _context.KeyPerformanceIndices.Include(k => k.SubCompetency)
                                                            .FirstOrDefault(k=>k.SubCompetency.Id == k.SubCompetencyId).SubCompetency.Name,
                    AchievementDate = a.AchievementDate,
                    Score = (int)a.Score
                })
                .ToList();

            return topAchieversWithKPI;

        }

        public async Task<List<TopScorerModel>> GetTopAchieversBySubCompetencyId(int subCompetencyId)
        {

            var topAchievers = await _context.EmployeeAchievements
                       .Where(e => e.Kpi.SubCompetencyId == subCompetencyId)
                       .Join(_context.KPISuccessCriteria,
                         ea => ea.KpiId,
                         kpisc => kpisc.KpiId,
                         (ea, kpisc) => new { EmployeeAchievement = ea, KPISuccessCriteria = kpisc })

                      .GroupBy(e => e.EmployeeAchievement.EmployeeId)
                       .Select(g => new
                       {
                           EmployeeId = g.Key,
                           AchievementDate = g.Max(e => e.EmployeeAchievement.AchievedOn),
                           Score = g.Sum(e => e.KPISuccessCriteria.Weightage) * g.Select(e => e.EmployeeAchievement.KpiId).Distinct().Count()
                       })
                       .OrderByDescending(a => a.Score)
                          .Select(a => new TopScorerModel
                          {
                              EmployeeId = a.EmployeeId,
                              EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == a.EmployeeId).Name,
                              KpiId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Id,
                              KpiName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Name,
                              SubCompetencyId = subCompetencyId,
                              SubCompetencyName = _context.SubCompetencies.FirstOrDefault(x => x.Id == subCompetencyId).Name,
                              GradeName = _context.Employees.FirstOrDefault(k => k.Id == a.EmployeeId).Grade.Name,
                              AchievementDate = a.AchievementDate,
                              StreamId = _context.KeyPerformanceIndices.Include(x => x.Stream).FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Stream.Id,
                              StreamName = _context.KeyPerformanceIndices.Include(x => x.Stream).FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Stream.Name,
                              Score = (int)a.Score
                          }).Take(5)
                             .ToListAsync();
            return topAchievers;
        }

        //returning all topscorers by subcompetency irrespective of subcompetency id
        public async Task<List<TopScorerModel>> GetTopAchieversBySubCompetencies()
        {

            var topAchievers = new List<TopScorerModel>();
            // Retrieve all sub-competencies
            var subCompetencies = await _context.SubCompetencies.ToListAsync();

            foreach (var subCompetency in subCompetencies)
            {
                var subCompetencyId = subCompetency.Id;

                // Fetch top achievers for each sub-competency
                var topAchieversForSubCompetency = await _context.EmployeeAchievements
                    .Where(ea => ea.Kpi.SubCompetencyId == subCompetencyId)
                    .Join(_context.KPISuccessCriteria,
                         ea => ea.KpiId,
                         kpisc => kpisc.KpiId,
                         (ea, kpisc) => new { EmployeeAchievement = ea, KPISuccessCriteria = kpisc })
                    
                   .GroupBy(e => e.EmployeeAchievement.EmployeeId)
                   .Select(g => new
                   {
                       EmployeeId = g.Key,
                       AchievementDate = g.Max(e => e.EmployeeAchievement.AchievedOn),
                       Score = g.Sum(e => e.KPISuccessCriteria.Weightage) * g.Select(e => e.EmployeeAchievement.KpiId).Distinct().Count()
                   })
                    .OrderByDescending(a => a.Score)
                    .Select(topAchievers => new TopScorerModel
                    {
                        EmployeeId = topAchievers.EmployeeId,
                        EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == topAchievers.EmployeeId).Name,
                        KpiId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Id,
                        KpiName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Name,
                        SubCompetencyId = subCompetencyId,
                        SubCompetencyName = subCompetency.Name,
                        GradeName = _context.Employees.FirstOrDefault(e => e.Id == topAchievers.EmployeeId).Grade.Name,
                        AchievementDate = topAchievers.AchievementDate,
                        Score = (int)topAchievers.Score
                    }).ToListAsync();

                // Add top achievers for the current sub-competency to the main list
                topAchievers.AddRange(topAchieversForSubCompetency);
            }

            return topAchievers;
        }

        public async Task<List<TopScorerModel>> GetTopAchievers()
        {
            var topAchievers = new List<TopScorerModel>();

            // Retrieve all sub-competencies and KPIs
            var subCompetencies = await _context.SubCompetencies.ToListAsync();
            var kpis = await _context.KeyPerformanceIndices.ToListAsync();

            // Dictionary to hold top achievers by sub-competency and KPI
            var topAchieversDict = new Dictionary<int, List<TopScorerModel>>();

            // Process sub-competencies
            foreach (var subCompetency in subCompetencies)
            {
                var subCompetencyId = subCompetency.Id;

                var topAchieversForSubCompetency = await _context.EmployeeAchievements
                    .Where(ea => ea.Kpi.SubCompetencyId == subCompetencyId)
                    .Join(_context.KPISuccessCriteria,
                         ea => ea.KpiId,
                         kpisc => kpisc.KpiId,
                         (ea, kpisc) => new { EmployeeAchievement = ea, KPISuccessCriteria = kpisc })
                    .GroupBy(e => e.EmployeeAchievement.EmployeeId)
                    .Select(g => new
                    {
                        EmployeeId = g.Key,
                        AchievementDate = g.Max(e => e.EmployeeAchievement.AchievedOn),
                        Score = g.Sum(e => e.KPISuccessCriteria.Weightage) * g.Select(e => e.EmployeeAchievement.KpiId).Distinct().Count()
                    })
                    .OrderByDescending(a => a.Score)
                    .Select(topAchiever => new TopScorerModel
                    {
                        EmployeeId = topAchiever.EmployeeId,
                        EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == topAchiever.EmployeeId).Name,
                        KpiId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Id,
                        KpiName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.SubCompetencyId == subCompetencyId).Name,
                        SubCompetencyId = subCompetencyId,
                        SubCompetencyName = subCompetency.Name,
                        StreamName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Stream.Id == k.StreamId).Stream.Name,
                        GradeName = _context.Employees.FirstOrDefault(e => e.Id== topAchiever.EmployeeId).Grade.Name,
                        AchievementDate = topAchiever.AchievementDate,
                        Score = (int)topAchiever.Score
                    }).ToListAsync();

                // Add to the dictionary
                topAchieversDict[subCompetencyId] = topAchieversForSubCompetency;
            }

            // Process KPIs
            foreach (var kpi in kpis)
            {
                var kpiId = kpi.Id;

                var topAchieversForKpi = await _context.EmployeeAchievements
                    .Where(e => e.KpiId == kpiId)
                    .Join(_context.KPISuccessCriteria,
                         ea => ea.KpiId,
                         kpisc => kpisc.KpiId,
                         (ea, kpisc) => new { EmployeeAchievement = ea, KPISuccessCriteria = kpisc })
                    .GroupBy(e => e.EmployeeAchievement.EmployeeId)
                    .Select(g => new
                    {
                        EmployeeId = g.Key,
                        AchievementDate = g.Max(e => e.EmployeeAchievement.AchievedOn),
                        Score = g.Sum(e => e.KPISuccessCriteria.Weightage) * g.Select(e => e.EmployeeAchievement.KpiId).Distinct().Count()
                    })
                    .OrderByDescending(a => a.Score)
                    .Select(a => new TopScorerModel
                    {
                        EmployeeId = a.EmployeeId,
                        EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == a.EmployeeId).Name,
                        KpiId = kpiId,
                        KpiName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == kpiId).Name,
                        GradeName = _context.Employees.FirstOrDefault(e => e.Id == a.EmployeeId).Grade.Name,
                        SubCompetencyId = _context.SubCompetencies.FirstOrDefault(x => x.Id == kpi.SubCompetencyId).Id,
                        SubCompetencyName = _context.SubCompetencies.FirstOrDefault(x => x.Id == kpi.SubCompetencyId).Name,
                        StreamId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == kpiId).Stream.Id,
                        StreamName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == kpiId).Stream.Name,
                        AchievementDate = a.AchievementDate,
                        Score = (int)a.Score
                    }).ToListAsync();

                // Add to the dictionary
                topAchieversDict[kpiId] = topAchieversForKpi;
            }

            // Consolidate results
            foreach (var achievers in topAchieversDict.Values)
            {
                topAchievers.AddRange(achievers);
            }

            return topAchievers;
        }
        public async Task<List<TopScorerModel>> GetTopAchieversForAllKpis()
        {
            var topAchievers = new List<TopScorerModel>();
            // Retrieve all sub-competencies
            var Kpis = await _context.KeyPerformanceIndices.ToListAsync();
            foreach (var kpi in Kpis)
            {
                var KpiId = kpi.Id;
                // Fetch top achievers for all kpis
                var topAchieversForKpis = await _context.EmployeeAchievements
                    .Where(e => e.KpiId == KpiId)
                    .Join(_context.KPISuccessCriteria,
                         ea => ea.KpiId,
                         kpisc => kpisc.KpiId,
                         (ea, kpisc) => new { EmployeeAchievement = ea, KPISuccessCriteria = kpisc })
                   .GroupBy(e => e.EmployeeAchievement.EmployeeId)
                   .Select(g => new
                   {
                       EmployeeId = g.Key,
                       AchievementDate = g.Max(e => e.EmployeeAchievement.AchievedOn),
                       Score = g.Sum(e => e.KPISuccessCriteria.Weightage) * g.Select(e => e.EmployeeAchievement.KpiId).Distinct().Count()
                   })
                    .OrderByDescending(a => a.Score)
                    .Select(a => new TopScorerModel
                    {
                        EmployeeId = a.EmployeeId,
                        EmployeeName = _context.Employees.FirstOrDefault(emp => emp.Id == a.EmployeeId).Name,
                        KpiId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == KpiId).Id,
                        KpiName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == KpiId).Name,
                        GradeName = _context.Employees.FirstOrDefault(e => e.Id == a.EmployeeId).Grade.Name,
                        SubCompetencyId = _context.SubCompetencies.FirstOrDefault(x => x.Id == KpiId).Id,
                        SubCompetencyName = _context.SubCompetencies.FirstOrDefault(x => x.Id == KpiId).Name,
                        StreamId = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == KpiId).Stream.Id,
                        StreamName = _context.KeyPerformanceIndices.FirstOrDefault(k => k.Id == KpiId).Stream.Name,
                        AchievementDate = a.AchievementDate,
                        Score = (int)a.Score
                    }).ToListAsync();
                topAchievers.AddRange(topAchieversForKpis);
            }
            return topAchievers;
        }
    }
}
