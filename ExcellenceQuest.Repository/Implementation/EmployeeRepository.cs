namespace ExcellenceQuest.Repository.Implementation
{
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeeRepository : IEmployeeRespository
    {
        private readonly ExcellenceQuestContext _context;
        public EmployeeRepository(ExcellenceQuestContext context)
        {
            _context = context;
        }
        

        public async Task<User> GetRole(string email, string pass)
        {
            var user = await _context.Users.Where(x => x.UserName == email && x.Password == pass).FirstOrDefaultAsync();
            return user;
        }

        //
        public async Task<EmployeeModel> GetEmployeeDashboard(int empId)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == empId)
                .Include(e => e.EmployeeAchievements)
                .Include(e => e.Competency)
                .Include(e => e.SubCompetency)
                .FirstOrDefaultAsync();

            if (employee == null) return new EmployeeModel();

            var employeeAchievements = await _context.EmployeeAchievements
                .Where(ea => ea.EmployeeId == empId)
                .Include(ea => ea.Kpi)
                .ToListAsync();

            var badgeConfigurations = await _context.BadgeConfigurations
                .Where(bc => bc.GradeId == employee.GradeId)
                .ToListAsync();

            var employeeKpiCounts = employeeAchievements
                .GroupBy(ea => ea.KpiId)
                .Select(g => new EmployeeKpiViewModel
                {
                    KpiId = g.Key,
                    KpiName = g.First().Kpi.Name,
                    KpiCount = g.Count(),
                    KpiCriteria = badgeConfigurations.FirstOrDefault(bc => bc.KpiId == g.Key)?.KpiCriteria ?? 0,
                })
                .ToList();

            var badgeDetails = employeeKpiCounts
                .Select(employeeKpiCount => new BadgeDetailsModel
                {
                    KpiName = employeeKpiCount.KpiName,
                    BadgeId = GetBadgeId(employeeKpiCount, badgeConfigurations),
                    BadgeName = GetBadgeName(employeeKpiCount, badgeConfigurations),
                    KPIsRequired = GetKPIsRequired(employeeKpiCount, badgeConfigurations),
                    NextBadge = GetNextBadge(employeeKpiCount, badgeConfigurations),
                })
                .ToList();

            var response = new EmployeeModel
                {
                    Name = employee.Name,
                    Id = (int)employee.Id,
                    Designation = employee.Designation,
                    GradeId = employee.GradeId,
                    CompetencyId = employee.CompetencyId,
                    SubCompetencyId = employee.SubCompetencyId,
                    StreamId = employee.StreamId,
                    KpiDetails = employeeKpiCounts,
                    BadgeDetails = badgeDetails
                
            };

            return response;
        }

        // Helper methods to reduce complexity and improve readability
        private string GetBadgeName(EmployeeKpiViewModel employeeKpiCount, List<BadgeConfiguration> badgeConfigurations)
        {
            var badgeNameConfiguration = badgeConfigurations
                 .Join(_context.Badges,
                         bc => bc.BadgeId,
                         b => b.Id,
                         (bc, b) => new { BadgeConfiguration = bc, Badge = b })
                .Where(bc => bc.BadgeConfiguration.KpiId == employeeKpiCount.KpiId && employeeKpiCount.KpiCount <= bc.BadgeConfiguration.KpiCriteria)
                .OrderBy(bc => bc.BadgeConfiguration.KpiCriteria)
                .FirstOrDefault();
            return badgeNameConfiguration?.Badge?.Name ?? string.Empty;

        }

        //
        private int GetBadgeId(EmployeeKpiViewModel employeeKpiCount, List<BadgeConfiguration> badgeConfigurations)
        {
            var badgeNameConfiguration = badgeConfigurations
                 .Join(_context.Badges,
                         bc => bc.BadgeId,
                         b => b.Id,
                         (bc, b) => new { BadgeConfiguration = bc, Badge = b })
                .Where(bc => bc.BadgeConfiguration.KpiId == employeeKpiCount.KpiId && employeeKpiCount.KpiCount <= bc.BadgeConfiguration.KpiCriteria)
                .OrderBy(bc => bc.BadgeConfiguration.KpiCriteria)
                .FirstOrDefault();
            return badgeNameConfiguration?.Badge?.Id ?? 0;

        }

        private int GetKPIsRequired(EmployeeKpiViewModel employeeKpiCount, List<BadgeConfiguration> badgeConfigurations)
        {
            return badgeConfigurations
                .Where(bc => bc.KpiId == employeeKpiCount.KpiId)
                .OrderBy(bc => bc.KpiCriteria)
                .FirstOrDefault(bc => employeeKpiCount.KpiCount < bc.KpiCriteria + 1)?.KpiCriteria + 1 - employeeKpiCount.KpiCount ?? 0;
        }

        private string GetNextBadge(EmployeeKpiViewModel employeeKpiCount, List<BadgeConfiguration> badgeConfigurations)
        {
            var badgeNameConfiguration = badgeConfigurations
                 .Join(_context.Badges,
                         bc => bc.BadgeId,
                         b => b.Id,
                         (bc, b) => new { BadgeConfiguration = bc, Badge = b })
                .Where(bc => bc.BadgeConfiguration.KpiId == employeeKpiCount.KpiId && employeeKpiCount.KpiCount <= bc.BadgeConfiguration.KpiCriteria)
                .OrderBy(bc => bc.BadgeConfiguration.KpiCriteria);
            var nextBadge = badgeNameConfiguration.Skip(1).FirstOrDefault();
           

            return nextBadge?.Badge?.Name ?? string.Empty;
        }

    }
}

