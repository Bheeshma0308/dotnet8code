namespace ExcellenceQuest.Business.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BadgeConfigurationService : IBadgeConfigurationService
    {
        private readonly IBadgeConfigurationRepository _badgeRepository;
        private readonly IMapper Mapper;
        public BadgeConfigurationService(IBadgeConfigurationRepository badgeRepository, IMapper mapper)
        {
            _badgeRepository = badgeRepository;
            Mapper = mapper;
        }
        public async Task Delete(int id)
        {
                await _badgeRepository.DeleteAsync(id);
        }

        public async Task<BadgeConfigurationModel> Save(BadgeConfigurationModel model)
        {
                if (model.Id > 0)
                {
                    return await _badgeRepository.UpdateAsync(model);

                }
                else
                {
                    return await _badgeRepository.AddAsync(model);
                }
         }

        public async Task<List<BadgeConfigurationModel>> GetAllBadges(int gradeId)
        {
            var result = await _badgeRepository.GetBadgesAsync(gradeId);
            return result;
        }
     }
    
}
