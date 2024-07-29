namespace ExcellenceQuest.API.Controllers
{
    using ExcellenceQuest.API.Helper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Business.Implementation;
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAchievementController : ControllerBase
    {
        private IEmployeeAchievementService _employeeAchievementService;
        public EmployeeAchievementController( IEmployeeAchievementService employeeAchievementService)
        {
             _employeeAchievementService = employeeAchievementService;
                
        }
        [HttpPost("SaveSelfReport")]
        public async Task<IActionResult> SaveSelfReport(EmployeeAchievementModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e?.ErrorMessage));
                    return Ok(ApiResponse<string>.BadRequest(string.Join(", ", errors)));
                }
                var res = await _employeeAchievementService.Save(obj); ;
                return Ok(ApiResponse<EmployeeAchievementModel>.Success(res, ConstMsg.Saved));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }


        }


        [HttpGet("GetEmployeeKpiCount")]
        public async Task<IActionResult> GetEmployeeKpiCount(int empId)
        {
            try
            {
                if (empId > 0)
                {
                    var data = await _employeeAchievementService.GetEmployeeKpiCount(empId);
                    if (data == null || !data.Any())
                    {
                        return Ok(ApiResponse<string>.Success(ConstMsg.NoData));
                    }
                    return Ok(ApiResponse<List<EmployeeKpiViewModel>>.Success(data));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));
            }
        }
    }
}
