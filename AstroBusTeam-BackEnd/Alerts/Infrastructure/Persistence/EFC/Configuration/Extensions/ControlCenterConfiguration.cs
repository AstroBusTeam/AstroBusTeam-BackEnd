using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for control centers
/// </summary>
public static class ControlCenterConfiguration
{
    /// <summary>
    ///     Applies the control center configuration to the model.
    /// </summary>
    public static void ApplyControlCenterConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ControlCenter>().HasKey(e => e.Id);
        builder.Entity<ControlCenter>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ControlCenter>().Property(e => e.Code)
            .HasMaxLength(40)
            .IsRequired();
        builder.Entity<ControlCenter>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<ControlCenter>().Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<ControlCenter>().Property(e => e.Zone)
            .HasMaxLength(60)
            .IsRequired();
    }
}
