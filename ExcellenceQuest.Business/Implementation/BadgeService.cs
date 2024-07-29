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

    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;
        private readonly IMapper Mapper;
        public BadgeService(IBadgeRepository badgeRepository, IMapper mapper)
        {
            _badgeRepository = badgeRepository;
            Mapper = mapper;
        }
        public async Task Delete(int id)
        {
                await _badgeRepository.DeleteAsync(id);
        }
       
        public async Task<BadgeModel> Save(BadgeModel model)
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
    }
}
