namespace ExcellenceQuest.Repository.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BadgeConfigurationRepository : GenericRepository<BadgeConfiguration, BadgeConfigurationModel>, IBadgeConfigurationRepository
    {
        private readonly ExcellenceQuestContext _context;
        private readonly IMapper Mapper;
        public BadgeConfigurationRepository(ExcellenceQuestContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            Mapper = mapper;

        }
        public  async Task<List<BadgeConfigurationModel>> GetBadgesAsync(int gradeId)
        {
             
            var badges = await _context.BadgeConfigurations
                                 .Where(b => b.GradeId == gradeId)
                                 .Select(bc => new  BadgeConfigurationModel
                                 {
                                     Id = bc.Id,
                                     BadgeId = bc.BadgeId,   
                                     BadgeName = bc.Badge.Name,
                                     KpiId = bc.KpiId,
                                     SubCompetencyId = bc.SubCompetencyId,
                                     GradeId = gradeId,
                                     KpiCriteria = bc.KpiCriteria,
                                     

                                 })
                                 .ToListAsync();

            return badges;        
        }
    }
}
