namespace ExcellenceQuest.Repository.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Models.Common;
    using ExcellenceQuest.Repository.Common;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BadgeRepository : GenericRepository<Badge, BadgeModel>, IBadgeRepository
    {
        private readonly ExcellenceQuestContext _context;
        private readonly IMapper Mapper;
        public BadgeRepository(ExcellenceQuestContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            Mapper = mapper;

        }
        
    }
}
