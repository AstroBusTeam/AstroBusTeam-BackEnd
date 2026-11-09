using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Operations.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions of the Operations bounded context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Operations configuration to the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyOperationsConfiguration(this ModelBuilder builder)
    {
        // Shift
        builder.Entity<Shift>().Property(s => s.ShiftDate)
            .HasConversion(
                value => value.ToDateTime(TimeOnly.MinValue),
                value => DateOnly.FromDateTime(value))
            .HasColumnType("date")
            .IsRequired();
        builder.Entity<Shift>().HasKey(s => s.Id);
        builder.Entity<Shift>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shift>().Property(s => s.DistanceKm)
            .HasColumnType("decimal(7,2)");
        builder.Entity<Shift>().Property(s => s.Revenue)
            .HasPrecision(10, 2);
        builder.Entity<Shift>().Property(s => s.AverageSpeedKmh)
            .HasColumnType("decimal(5,2)");
        builder.Entity<Shift>().Property(s => s.MaxSpeedKmh)
            .HasColumnType("decimal(5,1)");
        builder.Entity<Shift>().Property(s => s.OverspeedEvents)
            .HasColumnType("smallint");
        builder.Entity<Shift>().Property(s => s.Status)
            .HasConversion(valueObject => valueObject.Value, value => new ShiftStatus(value))
            .HasColumnType("enum('active','finished','interrupted')")
            .IsRequired();
        builder.Entity<Shift>()
            .HasIndex(s => new { s.DriverId, s.Status });

        builder.ApplyVehiclePositionConfiguration();
    }
}
