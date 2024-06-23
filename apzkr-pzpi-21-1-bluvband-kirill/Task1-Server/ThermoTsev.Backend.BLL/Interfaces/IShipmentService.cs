using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IShipmentService
{
    Result<List<Shipment>> GetAllShipments();

    Result<Shipment> GetShipmentById(int id);

    Result<CreateShipmentDto> CreateShipment(int userId, CreateShipmentDto shipment);

    Result<UpdateShipmentDto> UpdateShipment(UpdateShipmentDto updatedShipment);

    Result DeleteShipment(int id);

    List<Shipment> GetShipmentsByStatus(ShipmentStatus status);
}
