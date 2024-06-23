namespace ThermoTsev.Backend.Domain.Entities;

public class DeliveryLocation : BaseEntity
{
    public float Latitude { get; set; }

    public float Longitude { get; set; }

    public List<Shipment> OriginatingShipments { get; set; }

    public List<Shipment> DestinationShipments { get; set; }
}
