namespace ExcellenceQuest.Business.Contracts
{
    using ExcellenceQuest.Models.Badge;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBadgeService
    {
        Task<BadgeModel> Save(BadgeModel model);
        Task Delete(int id);
    }
}
