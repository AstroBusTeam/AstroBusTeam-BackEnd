using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
/// <remarks>
///     A single context is shared by every bounded context. Each bounded context contributes its own model
///     configuration through an <c>Apply{Context}Configuration</c> extension method.
/// </remarks>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyFleetConfiguration();
        builder.ApplyOperationsConfiguration();
        builder.ApplyAlertsConfiguration();
        builder.ApplyIamConfiguration();

        // Foreign keys between tables and bounded contexts. The aggregates keep only the identifier of the
        // related aggregate (no navigation properties), so each relationship is declared here with HasOne<T>().
        // Restrict: a record that is still referenced cannot be deleted.
        builder.Entity<Assignment>()
            .HasOne<Driver>().WithMany()
            .HasForeignKey(assignment => assignment.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Assignment>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(assignment => assignment.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Assignment>()
            .HasOne<BusRoute>().WithMany()
            .HasForeignKey(assignment => assignment.RouteId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Assignment>()
            .HasOne<User>().WithMany()
            .HasForeignKey(assignment => assignment.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Shift>()
            .HasOne<Assignment>().WithMany()
            .HasForeignKey(shift => shift.AssignmentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Shift>()
            .HasOne<Driver>().WithMany()
            .HasForeignKey(shift => shift.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Shift>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(shift => shift.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Shift>()
            .HasOne<BusRoute>().WithMany()
            .HasForeignKey(shift => shift.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VehiclePosition>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(position => position.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<VehiclePosition>()
            .HasOne<Shift>().WithMany()
            .HasForeignKey(position => position.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Alert>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(alert => alert.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<Driver>().WithMany()
            .HasForeignKey(alert => alert.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<Shift>().WithMany()
            .HasForeignKey(alert => alert.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<AlertType>().WithMany()
            .HasForeignKey(alert => alert.AlertTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<Recipient>().WithMany()
            .HasForeignKey(alert => alert.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<User>().WithMany()
            .HasForeignKey(alert => alert.ConfirmedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AlertAttempt>()
            .HasOne<Alert>().WithMany()
            .HasForeignKey(attempt => attempt.AlertId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Delivery>()
            .HasOne<Alert>().WithMany()
            .HasForeignKey(delivery => delivery.AlertId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Delivery>()
            .HasOne<Recipient>().WithMany()
            .HasForeignKey(delivery => delivery.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Delivery>()
            .HasOne<NotificationChannel>().WithMany()
            .HasForeignKey(delivery => delivery.ChannelId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Delivery>()
            .HasOne<AlertType>().WithMany()
            .HasForeignKey(delivery => delivery.AlertTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Recipient>()
            .HasOne<ControlCenter>().WithMany()
            .HasForeignKey(recipient => recipient.ControlCenterId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<RecipientChannel>()
            .HasOne<NotificationChannel>().WithMany()
            .HasForeignKey(link => link.ChannelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .HasOne<Driver>().WithMany()
            .HasForeignKey(user => user.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        // The identifiers are INT in C# and BIGINT in the database.
        foreach (var property in builder.Model.GetEntityTypes().SelectMany(entity => entity.GetProperties()))
        {
            var clrType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            if (clrType != typeof(int)) continue;
            if (property.Name == "Id" || property.Name.EndsWith("Id", StringComparison.Ordinal) ||
                property.Name is "AssignedBy" or "ConfirmedBy")
                property.SetColumnType("bigint");
        }

        builder.UseSnakeCaseNamingConvention();
    }
}
