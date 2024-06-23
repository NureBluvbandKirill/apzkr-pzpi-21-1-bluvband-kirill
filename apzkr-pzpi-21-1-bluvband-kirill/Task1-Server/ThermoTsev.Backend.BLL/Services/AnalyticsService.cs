using FluentResults;
using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Analytic;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class AnalyticsService(DataContext context) : IAnalyticsService
{
    public async Task<Result<AnalyticsDetailDto>> GetAnalyticByIdAsync(int analyticsDetailId)
    {
        AnalyticsDetail? analytic = await context.Analytics
            .FindAsync(analyticsDetailId);

        return analytic == null
            ? Result.Fail<AnalyticsDetailDto>($"Analytic with id {analyticsDetailId} not found.")
            : Result.Ok(
                new AnalyticsDetailDto(
                    analytic.MetricTitle,
                    analytic.MetricValue,
                    analytic.Timestamp
                )
            );
    }

    public async Task<Result<List<AnalyticsDetailDto>>> GetAnalyticsAsync()
    {
        List<AnalyticsDetail> analytics = await context.Analytics
            .ToListAsync();

        return Result.Ok(
            analytics.Select(
                    a => new AnalyticsDetailDto(
                        a.MetricTitle,
                        a.MetricValue,
                        a.Timestamp
                    )
                )
                .ToList()
        );
    }

    public async Task<Result<AnalyticsDetailDto>> CreateAnalyticsDetailAsync(int shipmentId, AnalyticsDetailDto analyticsDetailDto)
    {
        Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

        if (shipment is null)
        {
            return Result.Fail<AnalyticsDetailDto>($"Shipment with id {shipmentId} not found.");
        }

        AnalyticsDetail analyticsDetail = new AnalyticsDetail()
        {
            MetricTitle = analyticsDetailDto.MetricTitle,
            MetricValue = analyticsDetailDto.MetricValue,
            Timestamp = analyticsDetailDto.Timestamp,
            Shipment = shipment,
        };
        context.Analytics.Add(analyticsDetail);
        await context.SaveChangesAsync();

        return Result.Ok(analyticsDetailDto);
    }

    public async Task<Result<AnalyticsDetailDto>> UpdateAnalyticAsync(int analyticsDetailId, AnalyticsDetailDto analyticsDetailDto)
    {
        AnalyticsDetail? existingAnalytic = await context.Analytics
            .FindAsync(analyticsDetailId);

        if (existingAnalytic == null)
        {
            return Result.Fail<AnalyticsDetailDto>($"Analytic with id {analyticsDetailId} not found.");
        }

        existingAnalytic.MetricTitle = analyticsDetailDto.MetricTitle;
        existingAnalytic.MetricValue = analyticsDetailDto.MetricValue;
        existingAnalytic.Timestamp = analyticsDetailDto.Timestamp;
        await context.SaveChangesAsync();

        return Result.Ok(analyticsDetailDto);
    }

    public async Task<Result> DeleteAnalyticsDetailAsync(int analyticsDetailId)
    {
        AnalyticsDetail? analytic = await context.Analytics
            .FindAsync(analyticsDetailId);

        if (analytic == null)
        {
            return Result.Fail($"Analytic with id {analyticsDetailId} not found.");
        }

        context.Analytics.Remove(analytic);
        await context.SaveChangesAsync();

        return Result.Ok();
    }
}
