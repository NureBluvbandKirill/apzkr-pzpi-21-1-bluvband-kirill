using FluentResults;
using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Notification;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class EmergencyNotificationService(DataContext context) : IEmergencyNotificationService
{
    public async Task<Result<EmergencyNotificationDto>> GetEmergencyNotificationById(int emergencyNotificationId)
    {
        EmergencyNotification? notification = await context.EmergencyNotifications
            .FindAsync(emergencyNotificationId);

        return notification == null ? Result.Fail<EmergencyNotificationDto>($"Notification with id {emergencyNotificationId} not found.") : Result.Ok(new EmergencyNotificationDto(notification.Message, notification.IsRead));
    }

    public async Task<Result<List<EmergencyNotificationDto>>> GetAllEmergencyNotifications()
    {
        List<EmergencyNotification> notifications = await context.EmergencyNotifications
            .ToListAsync();

        return Result.Ok(
            notifications.Select(n => new EmergencyNotificationDto(n.Message, n.IsRead))
                .ToList()
        );
    }

    public async Task<Result<EmergencyNotificationDto>> CreateEmergencyNotification(int userId, EmergencyNotificationDto emergencyNotificationDto)
    {
        User? foundedUser = context.Users.FirstOrDefault(u => u.Id == userId);
        EmergencyNotification emergencyNotification = new EmergencyNotification()
        {
            Message = emergencyNotificationDto.Message,
            IsRead = emergencyNotificationDto.IsRead,
            User = foundedUser,
        };

        context.EmergencyNotifications.Add(emergencyNotification);
        await context.SaveChangesAsync();

        return Result.Ok(new EmergencyNotificationDto(emergencyNotification.Message, emergencyNotification.IsRead));
    }

    public async Task<Result<EmergencyNotificationDto>> UpdateEmergencyNotification(int emergencyNotificationId, EmergencyNotificationDto emergencyNotificationDto)
    {
        EmergencyNotification? existingNotification = await context.EmergencyNotifications
            .FindAsync(emergencyNotificationId);

        if (existingNotification == null)
        {
            return Result.Fail<EmergencyNotificationDto>($"Notification with id {emergencyNotificationId} not found.");
        }

        existingNotification.Message = emergencyNotificationDto.Message;
        existingNotification.IsRead = emergencyNotificationDto.IsRead;
        await context.SaveChangesAsync();

        return Result.Ok(new EmergencyNotificationDto(existingNotification.Message, existingNotification.IsRead));
    }

    public async Task<Result> DeleteEmergencyNotification(int emergencyNotificationId)
    {
        EmergencyNotification? notification = await context.EmergencyNotifications
            .FindAsync(emergencyNotificationId);

        if (notification == null)
        {
            return Result.Fail($"Notification with id {emergencyNotificationId} not found.");
        }

        context.EmergencyNotifications.Remove(notification);
        await context.SaveChangesAsync();

        return Result.Ok();
    }
}
