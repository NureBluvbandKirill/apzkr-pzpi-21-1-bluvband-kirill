namespace ThermoTsev.Backend.Domain.DTO.Shipment;

public record CreateShipmentDto
{
    public DateTime StartDate { get; set; }
    
    public float OriginatingDeliveryLocationLatitude { get; set; }
    
    public float OriginatingDeliveryLocationLongitude { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public float DestinationDeliveryLocationLatitude { get; set; }
    
    public float DestinationDeliveryLocationLongitude { get; set; }
    
    public float MinAllowedTemperature { get; set; }
    
    public float MaxAllowedTemperature { get; set; }
};
