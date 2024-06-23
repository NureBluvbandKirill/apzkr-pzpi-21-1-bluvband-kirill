using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.DAL;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<EmergencyNotification> EmergencyNotifications  { get; set; } = null!;

    public DbSet<Shipment> Shipments { get; set; } = null!;

    public DbSet<DeliveryLocation> DeliveryLocations { get; set; } = null!;

    public DbSet<ShipmentInfo> ShipmentInfos { get; set; } = null!;

    public DbSet<AnalyticsDetail> Analytics { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Shipment>()
            .HasOne(s => s.OriginatingDeliveryLocation)
            .WithMany(l => l.OriginatingShipments)
            .HasForeignKey(s => s.OriginatingDeliveryLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Shipment>()
            .HasOne(s => s.EndDeliveryLocation)
            .WithMany(l => l.DestinationShipments)
            .HasForeignKey(s => s.DestinationDeliveryLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ShipmentInfo>()
            .HasOne(sc => sc.Shipment)
            .WithOne(s => s.ShipmentInfo)
            .HasForeignKey<ShipmentInfo>(sc => sc.ShipmentId);
    }
}
