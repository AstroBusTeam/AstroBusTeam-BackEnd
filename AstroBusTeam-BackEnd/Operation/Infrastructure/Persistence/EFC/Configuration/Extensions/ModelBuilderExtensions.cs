using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Operations.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Operations bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Entity Framework configuration of the Operations bounded context.
    /// </summary>
    /// <param name="builder">The model builder</param>
    public static void ApplyOperationsConfiguration(this ModelBuilder builder)
    {
        // Shift
        builder.Entity<Shift>().Property(s => s.Date)
            .HasConversion(
                value => value.ToDateTime(TimeOnly.MinValue),
                value => DateOnly.FromDateTime(value))
            .HasColumnType("date")
            .IsRequired();
        builder.Entity<Shift>().HasKey(s => s.Id);
        builder.Entity<Shift>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shift>().Property(s => s.RouteCode)
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Shift>().Property(s => s.RouteName)
            .HasMaxLength(150)
            .IsRequired();
        builder.Entity<Shift>().Property(s => s.Revenue)
            .HasPrecision(10, 2);
        builder.Entity<Shift>().Property(s => s.Status)
            .HasConversion(valueObject => valueObject.Value, value => new ShiftStatus(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Shift>()
            .HasIndex(s => new { s.DriverId, s.Status });
    }
}
