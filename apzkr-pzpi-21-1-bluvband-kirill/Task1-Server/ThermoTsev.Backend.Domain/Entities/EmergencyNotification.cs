namespace ThermoTsev.Backend.Domain.Entities;

public class EmergencyNotification : BaseEntity
{
    public string Message { get; set; }

    public bool IsRead { get; set; }

    public User User { get; set; }
}
