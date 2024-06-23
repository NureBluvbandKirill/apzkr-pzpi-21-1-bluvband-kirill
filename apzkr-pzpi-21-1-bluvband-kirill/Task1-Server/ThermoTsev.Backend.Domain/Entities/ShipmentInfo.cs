namespace ThermoTsev.Backend.Domain.Entities;

public class ShipmentInfo : BaseEntity
{
    public float MinAllowedTemperature { get; set; }

    public float MaxAllowedTemperature { get; set; }

    public int ShipmentId { get; set; }

    public Shipment Shipment { get; init; }
}
