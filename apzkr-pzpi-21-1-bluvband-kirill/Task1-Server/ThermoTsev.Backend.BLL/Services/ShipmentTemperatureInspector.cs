using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Analytic;
using ThermoTsev.Backend.Domain.DTO.Notification;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class ShipmentTemperatureInspector(
    IServiceProvider serviceProvider
) : BackgroundService
{
    override async protected Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                List<int> shipmentIdsToProcess = GetShipmentsToProcess().ToList();

                foreach (int shipmentId in shipmentIdsToProcess)
                {
                    await ProcessShipment(shipmentId);
                }
            }
            catch
            {
                // do nothing
            }

            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }

    private IEnumerable<int> GetShipmentsToProcess()
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IShipmentService shipmentService = scope.ServiceProvider.GetRequiredService<IShipmentService>();

        List<Shipment> inTransitShipments = shipmentService.GetShipmentsByStatus(ShipmentStatus.InTransit);

        return inTransitShipments.Select(s => s.Id);
    }

    private async Task ProcessShipment(int shipmentId)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IIoTProviderService ioTProviderService = scope.ServiceProvider.GetRequiredService<IIoTProviderService>();
        IShipmentService shipmentService = scope.ServiceProvider.GetRequiredService<IShipmentService>();
        IAnalyticsService analyticsService = scope.ServiceProvider.GetRequiredService<IAnalyticsService>();

        Result<ShipmentInfoDto?> shipmentInfo = await ioTProviderService.GetCurrentShipmentInfo(shipmentId);
        Result<Shipment> shipment = shipmentService.GetShipmentById(shipmentId);

        if (shipmentInfo.IsSuccess && shipment.IsSuccess)
        {
            ShipmentInfoDto? shipmentInfoDto = shipmentInfo.Value;

            await analyticsService.CreateAnalyticsDetailAsync(shipmentId,
                new AnalyticsDetailDto("Temperature", shipmentInfoDto?.Temperature.ToString(CultureInfo.InvariantCulture) ?? string.Empty, DateTime.Now));

            if (shipmentInfoDto is not null)
            {
                await SendEmergencyNotificationIfNeeded(shipment.Value, shipmentInfoDto);
            }
        }
    }

    private async Task SendEmergencyNotificationIfNeeded(Shipment shipment, ShipmentInfoDto shipmentInfoDto)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IEmergencyNotificationService emergencyNotificationService = scope.ServiceProvider.GetRequiredService<IEmergencyNotificationService>();

        float temperatureDeviation = CalculateDeviation(shipmentInfoDto.Temperature,
            (shipment.ShipmentInfo.MinAllowedTemperature + shipment.ShipmentInfo.MaxAllowedTemperature) / 2);

        if (temperatureDeviation > 60)
        {
            string criticalMessage = $"Critical: Significant deviation in temperature for shipment {shipment.Id}.";
            await emergencyNotificationService.CreateEmergencyNotification(shipment.User.Id, new EmergencyNotificationDto(criticalMessage, false));
        }

        if (temperatureDeviation > 40)
        {
            string warningMessage = $"Warning: Deviation in temperature for shipment {shipment.Id}.";
            await emergencyNotificationService.CreateEmergencyNotification(shipment.User.Id, new EmergencyNotificationDto(warningMessage, false));
        }
    }

    private static float CalculateDeviation(float currentValue, float referenceValue) => Math.Abs(currentValue - referenceValue) / referenceValue * 100;
}
