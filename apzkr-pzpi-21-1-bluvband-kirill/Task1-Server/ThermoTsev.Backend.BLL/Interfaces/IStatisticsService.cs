using FluentResults;
using ThermoTsev.Backend.Domain.DTO.ShipmentCount;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IStatisticsService
{
    Result<List<ShipmentCountPerDayDto>> GetShipmentsStartCountPerDayLastMonth();

    Result<List<ShipmentCountPerDayDto>> GetShipmentsEndCountPerDayLastMonth();

    Result<int> GetShipmentCountLastWeek();

    Result<double> GetAverageShipmentsPerDay();

    Result<int> GetUserCount();

    Result<int> GetDeliveredShipmentCount();
}
