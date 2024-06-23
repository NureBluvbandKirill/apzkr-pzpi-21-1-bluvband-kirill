using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Shipment;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IIoTProviderService
{
    Task<Result<ShipmentLocationDto?>> GetCurrentShipmentLocation(int shipmentId);

    Task<Result<ShipmentInfoDto?>> GetCurrentShipmentInfo(int shipmentId);
}
