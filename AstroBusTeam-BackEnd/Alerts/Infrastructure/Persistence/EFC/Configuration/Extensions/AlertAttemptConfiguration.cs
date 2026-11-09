using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for alert attempts
/// </summary>
public static class AlertAttemptConfiguration
{
    /// <summary>
    ///     Applies the alert attempt configuration to the model.
    /// </summary>
    public static void ApplyAlertAttemptConfiguration(this ModelBuilder builder)
    {
        builder.Entity<AlertAttempt>().HasKey(e => e.Id);
        builder.Entity<AlertAttempt>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<AlertAttempt>().Property(e => e.AttemptNumber)
            .HasColumnType("tinyint");
        builder.Entity<AlertAttempt>().Property(e => e.Result)
            .HasColumnType("enum('success','failed','no_answer')")
            .IsRequired();
    }
}
