using FluentResults;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.ShipmentCount;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class StatisticsService(DataContext context) : IStatisticsService
{
    public Result<List<ShipmentCountPerDayDto>> GetShipmentsStartCountPerDayLastMonth()
    {
        try
        {
            DateTime lastMonthStartDate = DateTime.Today.AddMonths(-1);
            List<ShipmentCountPerDayDto> shipmentsCountPerDay = context.Shipments
                .Where(s => s.StartDate >= lastMonthStartDate)
                .GroupBy(s => s.StartDate.Date)
                .Select(
                    group => new ShipmentCountPerDayDto
                    {
                        Date = group.Key,
                        ShipmentCount = group.Count()
                    }
                )
                .OrderBy(dto => dto.Date)
                .ToList();

            return Result.Ok(shipmentsCountPerDay);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ShipmentCountPerDayDto>>(ex.Message);
        }
    }

    public Result<List<ShipmentCountPerDayDto>> GetShipmentsEndCountPerDayLastMonth()
    {
        try
        {
            DateTime lastMonthStartDate = DateTime.Today.AddMonths(-1);
            List<ShipmentCountPerDayDto> shipmentsCountPerDay = context.Shipments
                .Where(s => s.EndDate >= lastMonthStartDate)
                .GroupBy(s => s.EndDate)
                .Select(
                    group => new ShipmentCountPerDayDto
                    {
                        Date = group.Key,
                        ShipmentCount = group.Count()
                    }
                )
                .OrderBy(dto => dto.Date)
                .ToList();

            return Result.Ok(shipmentsCountPerDay);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ShipmentCountPerDayDto>>(ex.Message);
        }
    }

    public Result<int> GetShipmentCountLastWeek()
    {
        try
        {
            DateTime lastWeekStartDate = DateTime.Today.AddDays(-7);
            int shipmentCount = context.Shipments.Count(s => s.StartDate >= lastWeekStartDate);
            return Result.Ok(shipmentCount);
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(ex.Message);
        }
    }

    public Result<double> GetAverageShipmentsPerDay()
    {
        try
        {
            DateTime startDate = context.Shipments.Min(s => s.StartDate);
            DateTime endDate = DateTime.Today;
            double totalDays = (endDate - startDate).TotalDays;

            if (totalDays <= 0)
                return Result.Fail<double>("Invalid date range");

            double averageShipmentsPerDay = context.Shipments.Count() / totalDays;
            return Result.Ok(averageShipmentsPerDay);
        }
        catch (Exception ex)
        {
            return Result.Fail<double>(ex.Message);
        }
    }

    public Result<int> GetUserCount()
    {
        try
        {
            int userCount = context.Users.Count();
            return Result.Ok(userCount);
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(ex.Message);
        }
    }

    public Result<int> GetDeliveredShipmentCount()
    {
        try
        {
            int deliveredShipmentCount = context.Shipments.Count(s => s.Status == ShipmentStatus.Delivered);
            return Result.Ok(deliveredShipmentCount);
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(ex.Message);
        }
    }
}
