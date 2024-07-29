namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Models.Employee;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBadgeConfigurationService
    {
        Task<BadgeConfigurationModel> Save(BadgeConfigurationModel model);
        Task Delete(int id);
        Task<List<BadgeConfigurationModel>> GetAllBadges(int gradeId);
    }
}
