using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Notification;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IEmergencyNotificationService
{
    Task<Result<EmergencyNotificationDto>> GetEmergencyNotificationById(int emergencyNotificationId);

    Task<Result<List<EmergencyNotificationDto>>> GetAllEmergencyNotifications();

    Task<Result<EmergencyNotificationDto>> CreateEmergencyNotification(int userId, EmergencyNotificationDto emergencyNotificationDto);

    Task<Result<EmergencyNotificationDto>> UpdateEmergencyNotification(int emergencyNotificationId, EmergencyNotificationDto emergencyNotificationDto);

    Task<Result> DeleteEmergencyNotification(int emergencyNotificationId);
}
