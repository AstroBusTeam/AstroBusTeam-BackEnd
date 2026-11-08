using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for notification channels
/// </summary>
public static class NotificationChannelConfiguration
{
    /// <summary>
    ///     Applies the notification channel configuration to the model.
    /// </summary>
    public static void ApplyNotificationChannelConfiguration(this ModelBuilder builder)
    {
        builder.Entity<NotificationChannel>().HasKey(e => e.Id);
        builder.Entity<NotificationChannel>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NotificationChannel>().Property(e => e.Code)
            .HasColumnType("enum('sms','push','email','web','call')")
            .IsRequired();
        builder.Entity<NotificationChannel>().HasIndex(e => e.Code).IsUnique();
    }
}
