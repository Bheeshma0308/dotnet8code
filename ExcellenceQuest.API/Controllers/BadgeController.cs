namespace ExcellenceQuest.API.Controllers
{
    using ExcellenceQuest.API.Helper;
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Business.Implementation;
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Models.Employee;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _badgeService;
        private readonly IBadgeConfigurationService _badgeConfigurationService;
        public BadgeController(IBadgeService badgeService, IBadgeConfigurationService badgeConfigurationService)
        {
            _badgeService = badgeService;
            _badgeConfigurationService = badgeConfigurationService;
        }
        [HttpPost("SaveBadge")]
        public async Task<IActionResult> Save(BadgeModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e?.ErrorMessage));
                    return Ok(ApiResponse<string>.BadRequest(string.Join(", ", errors)));
                }
                var result = await _badgeService.Save(obj);
                return Ok(ApiResponse<BadgeModel>.Success(result, ConstMsg.Saved));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }


        }
        [HttpDelete("DeleteBadge")]
        public async Task<IActionResult> Delete(int badgeId)
        {
            try
            {
                if (badgeId > 0)
                {
                    await _badgeService.Delete(badgeId);
                    return Ok(ApiResponse<EmptyResult>.Success(null, ConstMsg.Deleted));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));

            }
        }
        [HttpPost("SaveBadgeConfig")]
        public async Task<IActionResult> SaveBadgeConfig(BadgeConfigurationModel obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e?.ErrorMessage));
                    return Ok(ApiResponse<string>.BadRequest(string.Join(", ", errors)));
                }
                var res = await _badgeConfigurationService.Save(obj);
                return Ok(ApiResponse<BadgeConfigurationModel>.Success(res, ConstMsg.Saved));
            }
            catch (Exception ex) { return Ok(ApiResponse<string>.Exception(ex.Message)); }
        }
        [HttpDelete("DeleteBadgeConfig")]
        public async Task<IActionResult> DeleteBadgeConfig(int badgeConfigId)
        {
            try
            {
                if (badgeConfigId > 0)
                {
                    await _badgeConfigurationService.Delete(badgeConfigId);
                    return Ok(ApiResponse<EmptyResult>.Success(null, ConstMsg.Deleted));
                }
                return Ok(ApiResponse<string>.BadRequest(ConstMsg.InvalidData));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.Exception(ex.Message));

            }
        }
        [HttpGet("GetAllBadges")]
        public async Task<IActionResult> GetBadges(int gradeId)
        {
            try
            {
                if (gradeId > 0)
                {
                    var res = await _badgeConfigurationService.GetAllBadges(gradeId);
                    return Ok(res);
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
