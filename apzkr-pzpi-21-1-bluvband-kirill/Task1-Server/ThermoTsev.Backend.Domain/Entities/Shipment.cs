using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.Domain.Entities;

public class Shipment : BaseEntity
{
    public DateTime StartDate { get; set; }

    public int OriginatingDeliveryLocationId { get; set; }

    public DeliveryLocation OriginatingDeliveryLocation { get; set; }

    public DateTime EndDate { get; set; }

    public int DestinationDeliveryLocationId { get; set; }

    public DeliveryLocation EndDeliveryLocation { get; set; }

    public ShipmentInfo ShipmentInfo { get; set; }

    public ShipmentStatus Status { get; set; }

    public User User { get; set; }

    public List<AnalyticsDetail> Analytics { get; set; } = [];
}
