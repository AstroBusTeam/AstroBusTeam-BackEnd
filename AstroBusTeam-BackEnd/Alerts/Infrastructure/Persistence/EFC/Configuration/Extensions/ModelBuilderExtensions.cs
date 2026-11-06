using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Alerts bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Entity Framework configuration of the Alerts bounded context.
    /// </summary>
    /// <param name="builder">The model builder</param>
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        // Alert
        builder.Entity<Alert>().HasKey(a => a.Id);
        builder.Entity<Alert>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(a => a.Code)
            .HasConversion(valueObject => valueObject.Value, value => new AlertCode(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Alert>().Property(a => a.Type)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Alert>().Property(a => a.Receiver)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Alert>().Property(a => a.Status)
            .HasConversion(valueObject => valueObject.Value, value => new AlertStatus(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Alert>()
            .HasIndex(a => a.Code)
            .IsUnique();
        builder.Entity<Alert>()
            .HasIndex(a => a.VehicleId);

        // Recipient
        builder.Entity<Recipient>().HasKey(r => r.Id);
        builder.Entity<Recipient>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Recipient>().Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Recipient>().Property(r => r.Role)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Recipient>().Property(r => r.Channel)
            .HasMaxLength(50)
            .IsRequired();

        // Delivery
        builder.Entity<Delivery>().HasKey(d => d.Id);
        builder.Entity<Delivery>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Delivery>().Property(d => d.RecipientName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.Channel)
            .HasMaxLength(50)
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.AlertType)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.Priority)
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.Status)
            .HasConversion(valueObject => valueObject.Value, value => new DeliveryStatus(value))
            .HasMaxLength(20)
            .IsRequired();
    }
}
