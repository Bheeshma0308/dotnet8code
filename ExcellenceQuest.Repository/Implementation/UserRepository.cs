namespace ExcellenceQuest.Repository.Implementation
{
    using System;
    using System.Threading.Tasks;
    using ExcellenceQuest.Repository.Contracts;
    using System.Collections.Generic;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    public class UserRepository : IUserRepository
    {
        private readonly ExcellenceQuestContext _context;
        private readonly IMapper Mapper;
        public UserRepository(ExcellenceQuestContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }
       
        public async Task<UserRoleModel> GetEmployeeRole(int employeeId)
        {
            var user = await _context.UserRoles.Where(x => x.EmployeeId == employeeId).Include(x=>x.Role).FirstOrDefaultAsync();

            return Mapper.Map<UserRoleModel>(user);

        }
        public async Task<UserModel> SearchUser(string userEmail, string password)
        {
            var res= await _context.Users.Where(x=>x.UserName == userEmail && x.Password==password).FirstOrDefaultAsync();
            
            return Mapper.Map<UserModel>(res);

        }
    }
}
