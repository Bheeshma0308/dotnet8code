namespace ExcellenceQuest.Repository.Contracts
{
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Repository.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBadgeConfigurationRepository : IGenericRepository<BadgeConfigurationModel>
    {
        Task<List<BadgeConfigurationModel>> GetBadgesAsync(int gradeId);
    }
}
