using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ThermoTsev.Backend.API.Attributes;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.ShipmentCount;

namespace ThermoTsev.Backend.API.Controllers;

[ApiController]
[AdminRoleAuthorize]
[Route("Api/[controller]")]
public class StatisticController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("ShipmentsStartCountPerDayLastMonth")]
    public IActionResult GetShipmentsStartCountPerDayLastMonth()
    {
        Result<List<ShipmentCountPerDayDto>> result = statisticsService.GetShipmentsStartCountPerDayLastMonth();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("ShipmentsEndCountPerDayLastMonth")]
    public IActionResult GetShipmentsEndCountPerDayLastMonth()
    {
        Result<List<ShipmentCountPerDayDto>> result = statisticsService.GetShipmentsEndCountPerDayLastMonth();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    
    [HttpGet("ShipmentCountLastWeek")]
    public IActionResult GetShipmentCountLastWeek()
    {
        Result<int> result = statisticsService.GetShipmentCountLastWeek();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("AverageShipmentsPerDay")]
    public IActionResult GetAverageShipmentsPerDay()
    {
        Result<double> result = statisticsService.GetAverageShipmentsPerDay();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("UserCount")]
    public IActionResult GetUserCount()
    {
        Result<int> result = statisticsService.GetUserCount();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("DeliveredShipmentCount")]
    public IActionResult GetDeliveredShipmentCount()
    {
        Result<int> result = statisticsService.GetDeliveredShipmentCount();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}
