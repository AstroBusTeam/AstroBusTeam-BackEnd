using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for routes
/// </summary>
public static class BusRouteConfiguration
{
    /// <summary>
    ///     Applies the route configuration to the model.
    /// </summary>
    public static void ApplyBusRouteConfiguration(this ModelBuilder builder)
    {
        builder.Entity<BusRoute>().ToTable("route");
        builder.Entity<BusRoute>().HasKey(e => e.Id);
        builder.Entity<BusRoute>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BusRoute>().Property(e => e.Code)
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<BusRoute>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<BusRoute>().Property(e => e.Name)
            .HasMaxLength(120)
            .IsRequired();
        builder.Entity<BusRoute>().Property(e => e.Origin)
            .HasMaxLength(120)
            .IsRequired();
        builder.Entity<BusRoute>().Property(e => e.Destination)
            .HasMaxLength(120)
            .IsRequired();
        builder.Entity<BusRoute>().Property(e => e.DistanceKm)
            .HasColumnType("decimal(6,2)");
    }
}
