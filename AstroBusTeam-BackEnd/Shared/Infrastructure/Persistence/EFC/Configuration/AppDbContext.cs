using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SecurityBus.API.Operations.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SecurityBus.API.Alerts.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Configuration;

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

        // Foreign keys between bounded contexts. The aggregates keep only the identifier of the related
        // aggregate (no navigation properties), so each relationship is declared here with HasOne<T>().
        // Restrict: a driver or vehicle that is still referenced cannot be deleted.
        builder.Entity<Assignment>()
            .HasOne<Driver>().WithMany()
            .HasForeignKey(assignment => assignment.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Assignment>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(assignment => assignment.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Alert>()
            .HasOne<Vehicle>().WithMany()
            .HasForeignKey(alert => alert.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        // TODO: when the Operations context is merged, relate Shift with Driver and Vehicle in the same way.
        builder.UseSnakeCaseNamingConvention();
    }
}
