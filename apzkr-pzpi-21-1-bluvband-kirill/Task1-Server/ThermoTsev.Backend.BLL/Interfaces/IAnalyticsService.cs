using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Analytic;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IAnalyticsService
{
    Task<Result<AnalyticsDetailDto>> GetAnalyticByIdAsync(int analyticsDetailId);

    Task<Result<List<AnalyticsDetailDto>>> GetAnalyticsAsync();

    Task<Result<AnalyticsDetailDto>> CreateAnalyticsDetailAsync(int shipmentId, AnalyticsDetailDto analyticsDetailDto);

    Task<Result<AnalyticsDetailDto>> UpdateAnalyticAsync(int analyticsDetailId, AnalyticsDetailDto analyticsDetailDto);

    Task<Result> DeleteAnalyticsDetailAsync(int analyticsDetailId);
}
