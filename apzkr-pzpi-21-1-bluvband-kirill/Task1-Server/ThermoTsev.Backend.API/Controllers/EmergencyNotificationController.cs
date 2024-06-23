using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Notification;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class EmergencyNotificationController(IEmergencyNotificationService emergencyNotificationService) : ControllerBase
{
    [HttpGet("{emergencyNotificationId:int}")]
    public async Task<IActionResult> GetEmergencyNotificationById(int emergencyNotificationId)
    {
        Result<EmergencyNotificationDto> result = await emergencyNotificationService.GetEmergencyNotificationById(emergencyNotificationId);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmergencyNotifications()
    {
        Result<List<EmergencyNotificationDto>> result = await emergencyNotificationService.GetAllEmergencyNotifications();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmergencyNotification([FromBody] EmergencyNotificationDto emergencyNotificationDto)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Result<EmergencyNotificationDto> result = await emergencyNotificationService.CreateEmergencyNotification(userId, emergencyNotificationDto);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPut("{emergencyNotificationId:int}")]
    public async Task<IActionResult> UpdateEmergencyNotification(int emergencyNotificationId, [FromBody] EmergencyNotificationDto emergencyNotificationDto)
    {
        Result<EmergencyNotificationDto> result = await emergencyNotificationService.UpdateEmergencyNotification(emergencyNotificationId, emergencyNotificationDto);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpDelete("{emergencyNotificationId:int}")]
    public async Task<IActionResult> DeleteEmergencyNotification(int emergencyNotificationId)
    {
        Result result = await emergencyNotificationService.DeleteEmergencyNotification(emergencyNotificationId);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
