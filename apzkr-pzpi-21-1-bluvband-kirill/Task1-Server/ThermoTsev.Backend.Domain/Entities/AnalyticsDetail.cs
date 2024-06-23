namespace ThermoTsev.Backend.Domain.Entities;

public class AnalyticsDetail : BaseEntity
{
    public string MetricTitle { get; set; }

    public string MetricValue { get; set; }

    public DateTime Timestamp { get; set; }

    public Shipment Shipment { get; init; }
}
