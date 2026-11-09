using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions of the Fleet bounded context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Fleet configuration to the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyFleetConfiguration(this ModelBuilder builder)
    {
        // Driver
        builder.Entity<Driver>().Property(d => d.LicenseExpiry)
            .HasConversion(
                value => value.ToDateTime(TimeOnly.MinValue),
                value => DateOnly.FromDateTime(value))
            .HasColumnType("date")
            .IsRequired();
        builder.Entity<Driver>().HasKey(d => d.Id);
        builder.Entity<Driver>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Driver>().Property(d => d.EmployeeCode)
            .HasConversion(valueObject => valueObject.Value, value => new EmployeeCode(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.FirstName)
            .HasMaxLength(80)
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.LastName)
            .HasMaxLength(80)
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.Dni)
            .HasConversion(valueObject => valueObject.Value, value => new Dni(value))
            .HasColumnType("char(8)")
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.LicenseCategory)
            .HasColumnType("enum('A-I','A-IIa','A-IIb','A-IIIa','A-IIIb','A-IIIc')")
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.LicenseNumber)
            .HasConversion(valueObject => valueObject.Value, value => new LicenseNumber(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Driver>().Property(d => d.Points)
            .HasColumnType("smallint");
        builder.Entity<Driver>().Property(d => d.Rating)
            .HasPrecision(2, 1);
        builder.Entity<Driver>().Property(d => d.YearsExperience)
            .HasColumnType("smallint");
        builder.Entity<Driver>().Property(d => d.Phone)
            .HasMaxLength(20);
        builder.Entity<Driver>().Property(d => d.Status)
            .HasConversion(valueObject => valueObject.Value, value => new DriverStatus(value))
            .HasColumnType("enum('active','inactive')")
            .IsRequired();
        builder.Entity<Driver>()
            .HasIndex(d => d.EmployeeCode)
            .IsUnique();
        builder.Entity<Driver>()
            .HasIndex(d => d.Dni)
            .IsUnique();

        // Vehicle
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(v => v.Code)
            .HasConversion(valueObject => valueObject.Value, value => new VehicleCode(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Plate)
            .HasConversion(valueObject => valueObject.Value, value => new LicensePlate(value))
            .HasMaxLength(10)
            .IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Model)
            .HasMaxLength(60)
            .IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Capacity)
            .HasColumnType("smallint");
        builder.Entity<Vehicle>().Property(v => v.Status)
            .HasConversion(valueObject => valueObject.Value, value => new VehicleStatus(value))
            .HasColumnType("enum('available','in-service','maintenance')")
            .IsRequired();
        builder.Entity<Vehicle>().Property(v => v.CurrentLatitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<Vehicle>().Property(v => v.CurrentLongitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<Vehicle>()
            .HasIndex(v => v.Code)
            .IsUnique();
        builder.Entity<Vehicle>()
            .HasIndex(v => v.Plate)
            .IsUnique();

        // Assignment
        builder.Entity<Assignment>().Property(a => a.StartTime)
            .HasConversion(
                value => value.ToTimeSpan(),
                value => TimeOnly.FromTimeSpan(value))
            .HasColumnType("time")
            .IsRequired();
        builder.Entity<Assignment>().Property(a => a.ValidFrom)
            .HasConversion(
                value => value.ToDateTime(TimeOnly.MinValue),
                value => DateOnly.FromDateTime(value))
            .HasColumnType("date")
            .IsRequired();
        builder.Entity<Assignment>().Property(a => a.ValidTo)
            .HasConversion(
                value => value.HasValue ? value.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                value => value.HasValue ? DateOnly.FromDateTime(value.Value) : (DateOnly?)null)
            .HasColumnType("date");
        builder.Entity<Assignment>().HasKey(a => a.Id);
        builder.Entity<Assignment>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Assignment>().Property(a => a.Status)
            .HasConversion(valueObject => valueObject.Value, value => new AssignmentStatus(value))
            .HasColumnType("enum('active','inactive')")
            .IsRequired();
        builder.Entity<Assignment>()
            .HasIndex(a => new { a.DriverId, a.Status });
        builder.Entity<Assignment>()
            .HasIndex(a => new { a.VehicleId, a.Status });
        builder.Entity<Assignment>()
            .HasIndex(a => a.ActiveDriverId)
            .IsUnique();
        builder.Entity<Assignment>()
            .HasIndex(a => a.ActiveVehicleId)
            .IsUnique();

        builder.ApplyBusRouteConfiguration();
    }
}
