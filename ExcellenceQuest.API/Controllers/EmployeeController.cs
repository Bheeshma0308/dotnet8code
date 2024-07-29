namespace ExcellenceQuest.API.Controllers
{
    using ExcellenceQuest.API.Helper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Business.Implementation;
    using ExcellenceQuest.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using ExcellenceQuest.Models.Employee;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService    _employeeService ;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
       
        

        [HttpGet("GetEmployeeDashboard")]
        public async Task<IActionResult> GetEmployeeDashBoard(int empId)
        {
            try
            {
                if (empId > 0)
                {
                    var res = await _employeeService.GetEmployeeDashboard(empId);
                    return Ok(ApiResponse<EmployeeModel>.Success(res));

                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }

           
        }

    }
}
