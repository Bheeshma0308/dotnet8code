namespace ExcellenceQuest.Business.Implementation
{
    using AutoMapper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRespository _employeeRepository;
        private readonly IEmployeeAchievementRepository _employeeAchievementRepository;
        private readonly IMapper Mapper;
        public EmployeeService(IEmployeeRespository employeeRepository, IEmployeeAchievementRepository employeeAchievementRepository, IMapper mapper)
        {
            _employeeAchievementRepository = employeeAchievementRepository;
            _employeeRepository = employeeRepository;
            Mapper = mapper;
        }
        public async Task<UserModel> GetRole(string email, string pass)
        {
            var res = await _employeeRepository.GetRole(email, pass);
            return Mapper.Map<UserModel>(res);
        }
        

       
        public async Task<EmployeeModel> GetEmployeeDashboard(int empId)
        {

            if (empId < 0)
            {
                throw new ArgumentException("Employee Id cannot be negative");
            }

            var result = await _employeeRepository.GetEmployeeDashboard(empId);
            return Mapper.Map<EmployeeModel>(result);

        }
    }
}

