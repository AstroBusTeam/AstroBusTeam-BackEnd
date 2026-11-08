using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Iam.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity configuration for users
/// </summary>
public static class UserConfiguration
{
    /// <summary>
    ///     Applies the user configuration to the model.
    /// </summary>
    public static void ApplyUserConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(e => e.Id);
        builder.Entity<User>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(e => e.Username)
            .HasMaxLength(50)
            .IsRequired();
        builder.Entity<User>().HasIndex(e => e.Username).IsUnique();
        builder.Entity<User>().Property(e => e.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();
        builder.Entity<User>().Property(e => e.Role)
            .HasColumnType("enum('admin','operator','driver')")
            .IsRequired();
    }
}
