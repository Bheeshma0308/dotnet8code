namespace ExcellenceQuest.API.Controllers
{
    using ExcellenceQuest.API.Helper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Models;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class KeyPerformanceIndexController : ControllerBase
    {
        private IKeyPerformanceIndexService _keyPerformanceIndexService;
        public KeyPerformanceIndexController(IKeyPerformanceIndexService keyPerformanceIndexService)
        {
            _keyPerformanceIndexService = keyPerformanceIndexService;
        }
        [HttpGet("GetKpiAll")]
        public async Task<IActionResult> GetKPIList()
        {
            try
            {
                var result = await _keyPerformanceIndexService.GetKPIList();
                if (result != null && result.Any())
                    return Ok(ApiResponse<List<KeyPerformanceIndexModel>>.Success(result));

                return Ok(ApiResponse<string>.Success(ConstMsg.NoData));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<string>.Exception(ex.Message));
            }
        }
        [HttpPost("SaveKpi")]
        public async Task<IActionResult> SaveKPI(KeyPerformanceIndexModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e?.ErrorMessage));
                    return Ok(ApiResponse<string>.BadRequest(string.Join(", ", errors)));
                }
                var res = await _keyPerformanceIndexService.SaveKPI(obj);
                return Ok(ApiResponse<KeyPerformanceIndexModel>.Success(res, ConstMsg.Saved));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }
        }

        [HttpGet("GetAchieversListByKpiId")]
        public async Task<IActionResult> GetAchieverListByKpiId(int kpiId)
        {
            try
            {
                if (kpiId > 0)
                {
                    var res = await _keyPerformanceIndexService.GetTopAchieverByKpiId(kpiId);
                    return Ok(ApiResponse<List<TopScorerModel>>.Success(res));

                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }
        }
        [HttpGet("GetAchieversListBySubCompetencyId")]
        public async Task<IActionResult> GetAchieverListBySubCompetencyId(int subcompetencyId)
        {
            try
            {
                if (subcompetencyId > 0)
                {
                    var res = await _keyPerformanceIndexService.GetTopAchieversBySubCompetencyId(subcompetencyId);
                    return Ok(ApiResponse<List<TopScorerModel>>.Success(res));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));
            }
        }
        [HttpDelete("DeleteKpi")]
        public async Task<IActionResult> Delete(int kpiId)
        {
            try
            {
                if (kpiId > 0)
                {
                    await _keyPerformanceIndexService.Delete(kpiId);
                    return Ok(ApiResponse<EmptyResult>.Success(null, ConstMsg.Deleted));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));

            }

        }
        [HttpGet("AllTopscorersBySubcompetency")]
        public async Task<IActionResult> GetTopAchieversBySubcompetencies()
        {
            try
            {
                var res = await _keyPerformanceIndexService.GetTopAchieversBySubCompetencies();
                return Ok(ApiResponse<List<TopScorerModel>>.Success(res));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));
            }

        }

        [HttpGet("AllTopscorers")]
        public async Task<IActionResult> GetTopAchievers()
        {
            try
            {
                var res = await _keyPerformanceIndexService.GetTopAchievers();
                return Ok(ApiResponse<List<TopScorerModel>>.Success(res));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));
            }
        }
        [HttpGet("AllTopScorersByKpi")]
        public async Task<IActionResult> GetAllAchieverList()
        {
            try
            {
                var res = await _keyPerformanceIndexService.GetTopAchieversForAllKpis();
                return Ok(ApiResponse<List<TopScorerModel>>.Success(res));
            }
            catch (System.Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));
            }
        }
    }
}
