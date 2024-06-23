using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class ShipmentService(DataContext context) : IShipmentService
{
    public Result<List<Shipment>> GetAllShipments()
    {
        try
        {
            List<Shipment> shipments = context.Shipments
                .Include(s => s.OriginatingDeliveryLocation)
                .Include(s => s.EndDeliveryLocation)
                .Include(s => s.ShipmentInfo)
                .ToList();
            return Result.Ok(shipments);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Shipment>>(ex.Message);
        }
    }

    public Result<Shipment> GetShipmentById(int id)
    {
        try
        {
            Shipment? shipment = context.Shipments
                .Include(s => s.OriginatingDeliveryLocation)
                .Include(s => s.EndDeliveryLocation)
                .Include(s => s.ShipmentInfo)
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);

            return shipment == null
                ? Result.Fail<Shipment>("Shipment not found")
                : Result.Ok(shipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<Shipment>(ex.Message);
        }
    }

    public Result<CreateShipmentDto> CreateShipment(int userId, CreateShipmentDto shipment)
    {
        try
        {
            EntityEntry<DeliveryLocation> startLocation = context.DeliveryLocations.Add(
                new DeliveryLocation()
                {
                    Latitude = shipment.OriginatingDeliveryLocationLatitude,
                    Longitude = shipment.OriginatingDeliveryLocationLongitude,
                }
            );

            EntityEntry<DeliveryLocation> endLocation = context.DeliveryLocations.Add(
                new DeliveryLocation()
                {
                    Latitude = shipment.DestinationDeliveryLocationLatitude,
                    Longitude = shipment.DestinationDeliveryLocationLongitude,
                }
            );

            EntityEntry<ShipmentInfo> shipmentCondition = context.ShipmentInfos.Add(
                new ShipmentInfo()
                {
                    MinAllowedTemperature = shipment.MinAllowedTemperature,
                    MaxAllowedTemperature = shipment.MaxAllowedTemperature,
                }
            );

            User? foundedUser = context.Users.FirstOrDefault(u => u.Id == userId);

            context.Shipments.Add(
                new Shipment()
                {
                    StartDate = shipment.StartDate,
                    OriginatingDeliveryLocation = startLocation.Entity,
                    EndDate = shipment.EndDate,
                    EndDeliveryLocation = endLocation.Entity,
                    ShipmentInfo = shipmentCondition.Entity,
                    Status = ShipmentStatus.Pending,
                    User = foundedUser,
                }
            );
            context.SaveChanges();
            return Result.Ok(shipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<CreateShipmentDto>(ex.Message);
        }
    }

    public Result<UpdateShipmentDto> UpdateShipment(UpdateShipmentDto updatedShipment)
    {
        try
        {
            Shipment? existingShipment = context.Shipments
                .Include(s => s.OriginatingDeliveryLocation)
                .Include(s => s.EndDeliveryLocation)
                .Include(s => s.ShipmentInfo)
                .FirstOrDefault(s => s.Id == updatedShipment.Id);

            if (existingShipment == null)
                return Result.Fail<UpdateShipmentDto>("Shipment not found");

            existingShipment.StartDate = updatedShipment.StartDate;
            existingShipment.OriginatingDeliveryLocation.Latitude = updatedShipment.OriginatingDeliveryLocationLatitude;
            existingShipment.OriginatingDeliveryLocation.Longitude = updatedShipment.OriginatingDeliveryLocationLongitude;
            existingShipment.EndDate = updatedShipment.EndDate;
            existingShipment.EndDeliveryLocation.Latitude = updatedShipment.DestinationDeliveryLocationLatitude;
            existingShipment.EndDeliveryLocation.Longitude = updatedShipment.DestinationDeliveryLocationLongitude;
            existingShipment.ShipmentInfo.MinAllowedTemperature = updatedShipment.MinAllowedTemperature;
            existingShipment.ShipmentInfo.MaxAllowedTemperature = updatedShipment.MaxAllowedTemperature;
            existingShipment.Status = updatedShipment.Status;

            context.SaveChanges();

            return Result.Ok(updatedShipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<UpdateShipmentDto>(ex.Message);
        }
    }

    public Result DeleteShipment(int id)
    {
        try
        {
            Shipment? shipmentToDelete = context.Shipments.Find(id);

            if (shipmentToDelete == null)
                return Result.Fail("Shipment not found");

            context.Shipments.Remove(shipmentToDelete);
            context.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public List<Shipment> GetShipmentsByStatus(ShipmentStatus status)
    {
        return context.Shipments
            .Where(s => s.Status == status)
            .ToList();
    }
}
