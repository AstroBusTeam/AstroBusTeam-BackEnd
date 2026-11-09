using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Operations.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for vehicle positions
/// </summary>
public static class VehiclePositionConfiguration
{
    /// <summary>
    ///     Applies the vehicle position configuration to the model.
    /// </summary>
    public static void ApplyVehiclePositionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<VehiclePosition>().HasKey(e => e.Id);
        builder.Entity<VehiclePosition>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<VehiclePosition>().Property(e => e.Latitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<VehiclePosition>().Property(e => e.Longitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<VehiclePosition>().Property(e => e.SpeedKmh)
            .HasColumnType("decimal(5,1)");
    }
}
