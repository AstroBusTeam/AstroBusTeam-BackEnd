using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for alert types
/// </summary>
public static class AlertTypeConfiguration
{
    /// <summary>
    ///     Applies the alert type configuration to the model.
    /// </summary>
    public static void ApplyAlertTypeConfiguration(this ModelBuilder builder)
    {
        builder.Entity<AlertType>().HasKey(e => e.Id);
        builder.Entity<AlertType>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<AlertType>().Property(e => e.Name)
            .HasMaxLength(60)
            .IsRequired();
        builder.Entity<AlertType>().HasIndex(e => e.Name).IsUnique();
        builder.Entity<AlertType>().Property(e => e.DefaultPriority)
            .HasColumnType("enum('low','medium','high','critical')")
            .IsRequired();
    }
}
