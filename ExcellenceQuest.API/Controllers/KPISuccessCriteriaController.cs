namespace ExcellenceQuest.API.Controllers
{
    using ExcellenceQuest.API.Helper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Business.Implementation;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Models.KPI;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class KPISuccessCriteriaController : ControllerBase
    {
        private IKPISuccessCriteriaService _kPISuccessCriteriaService;
        public KPISuccessCriteriaController(IKPISuccessCriteriaService kPISuccessCriteriaService)
        {
            _kPISuccessCriteriaService = kPISuccessCriteriaService;
        }
        [HttpPost("SaveSuccessCriteria")]
        public async Task<IActionResult> SaveKPI(KPISuccessCriteriaModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e?.ErrorMessage));
                    return Ok(ApiResponse<string>.BadRequest(string.Join(", ", errors)));
                }
                var res = await _kPISuccessCriteriaService.SaveKpiSuccessCriteria(obj);
                return Ok(ApiResponse<KPISuccessCriteriaModel>.Success(res, ConstMsg.Saved));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }


        }
        [HttpDelete("DeleteSuccessCriteria")]
        public async Task<IActionResult> Delete(int kpiId)
        {
            try
            {
                if (kpiId > 0)
                {
                    await _kPISuccessCriteriaService.Delete(kpiId);
                    return Ok(ApiResponse<EmptyResult>.Success(null, ConstMsg.Deleted));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));

            }
        }
        [HttpGet("GetKPISuccessCriteria")]
        public async Task<IActionResult> GetKPISuccessCriteria(int GradeId)
        {
            try
            {
                if (GradeId > 0)
                {
                    var res = await _kPISuccessCriteriaService.GetKPISuccessCriteriaDetails(GradeId);
                    return Ok(ApiResponse<List<KPISuccessCriteriaModel>>.Success(res));

                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }
        }
        [HttpGet("GetEmployeeKPISuccessCriteria")]
        public async Task<IActionResult> GetEmployeeKPICreiteria(int GradeId)
        {
            try
            {
                if (GradeId > 0)
                {
                    var res = await _kPISuccessCriteriaService.GetEmployeeKPICreiteria(GradeId);
                    return Ok(ApiResponse<List<KPICriteriaModel>>.Success(res));

                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }
        }
    }
}
