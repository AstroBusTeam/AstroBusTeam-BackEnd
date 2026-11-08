using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions of the Alerts bounded context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Alerts configuration to the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        // Alert
        builder.Entity<Alert>().HasKey(a => a.Id);
        builder.Entity<Alert>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(a => a.Code)
            .HasConversion(valueObject => valueObject.Value, value => new AlertCode(value))
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Alert>().Property(a => a.Attempts)
            .HasColumnType("tinyint");
        builder.Entity<Alert>().Property(a => a.Status)
            .HasConversion(valueObject => valueObject.Value, value => new AlertStatus(value))
            .HasColumnType("enum('pending','confirmed','critical')")
            .IsRequired();
        builder.Entity<Alert>().Property(a => a.Latitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<Alert>().Property(a => a.Longitude)
            .HasColumnType("decimal(9,6)");
        builder.Entity<Alert>().Property(a => a.Notes)
            .HasColumnType("text");
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
            .HasMaxLength(80)
            .IsRequired();
        builder.Entity<Recipient>().Property(r => r.Email)
            .HasMaxLength(120);
        builder.Entity<Recipient>().Property(r => r.Phone)
            .HasMaxLength(20);
        builder.Entity<Recipient>()
            .HasMany(r => r.Channels)
            .WithOne()
            .HasForeignKey(link => link.RecipientId)
            .OnDelete(DeleteBehavior.Cascade);

        // RecipientChannel (join table recipient_channels)
        builder.Entity<RecipientChannel>().HasKey(link => new { link.RecipientId, link.ChannelId });

        // Delivery
        builder.Entity<Delivery>().HasKey(d => d.Id);
        builder.Entity<Delivery>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Delivery>().Property(d => d.Priority)
            .HasColumnType("enum('low','medium','high','critical')")
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.Status)
            .HasConversion(valueObject => valueObject.Value, value => new DeliveryStatus(value))
            .HasColumnType("enum('sent','failed')")
            .IsRequired();
        builder.Entity<Delivery>().Property(d => d.ErrorMessage)
            .HasMaxLength(255);

        builder.ApplyAlertTypeConfiguration();
        builder.ApplyAlertAttemptConfiguration();
        builder.ApplyNotificationChannelConfiguration();
        builder.ApplyControlCenterConfiguration();
    }
}
