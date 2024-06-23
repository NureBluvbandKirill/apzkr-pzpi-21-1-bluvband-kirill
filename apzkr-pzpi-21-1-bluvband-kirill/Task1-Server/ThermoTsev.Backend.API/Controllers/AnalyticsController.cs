using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Analytic;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("{analyticsDetailId:int}")]
    public async Task<IActionResult> GetAnalyticsDetailById(int analyticsDetailId)
    {
        Result<AnalyticsDetailDto> result = await analyticsService.GetAnalyticByIdAsync(analyticsDetailId);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAnalytics()
    {
        Result<List<AnalyticsDetailDto>> result = await analyticsService.GetAnalyticsAsync();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPost("{shipmentId:int}")]
    public async Task<IActionResult> CreateAnalyticsDetail(int shipmentId, [FromBody] AnalyticsDetailDto analyticsDetailDto)
    {
        Result<AnalyticsDetailDto> result = await analyticsService.CreateAnalyticsDetailAsync(shipmentId, analyticsDetailDto);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPut("{analyticsDetailId:int}")]
    public async Task<IActionResult> UpdateAnalyticsDetail(int analyticsDetailId, [FromBody] AnalyticsDetailDto analyticsDetailDto)
    {
        Result<AnalyticsDetailDto> result = await analyticsService.UpdateAnalyticAsync(analyticsDetailId, analyticsDetailDto);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpDelete("{analyticsDetailId:int}")]
    public async Task<IActionResult> DeleteAnalyticsDetail(int analyticsDetailId)
    {
        Result result = await analyticsService.DeleteAnalyticsDetailAsync(analyticsDetailId);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
