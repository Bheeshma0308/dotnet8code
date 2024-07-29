namespace ExcellenceQuest.Repository.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Models.KPI;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class KPISuccessCriteriaRepository : GenericRepository<KPISuccessCriterion, KPISuccessCriteriaModel>, IKPISuccessCriteriaRepository
    {
        private readonly ExcellenceQuestContext _context;
        private readonly IMapper Mapper;
        public KPISuccessCriteriaRepository(ExcellenceQuestContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            Mapper = mapper;

        }

        public async Task<List<KPISuccessCriteriaModel>> GetKPISuccessCriteriaDetails(int GradeId)
        {
            var response = await _context.KPISuccessCriteria
                .Where(kpisc => kpisc.GradeId == GradeId)
                .Select(x => new KPISuccessCriteriaModel
                {
                    GradeId = x.GradeId,
                    KpiId = x.KpiId,
                    SuccessCriteria = x.SuccessCriteria,
                    Weightage = x.Weightage,
                    Rating = x.Rating,
                    SubCompetencyId = x.SubCompetencyId
                })
                .ToListAsync();
            return response;
        }
        public async Task<List<KPICriteriaModel>> GetEmployeeKPICreiteria(int GradeId)
        {
            var response = await _context.KPISuccessCriteria
                .Where(kpisc => kpisc.GradeId == GradeId)
                .Select(k => new KPICriteriaModel
                {
                    GradeID = k.GradeId,
                    KpiId = k.KpiId,
                    KpiName = k.Kpi.Name,
                    Weightage = k.Weightage
                })
                .ToListAsync();
            return response;
        }

    }
}
