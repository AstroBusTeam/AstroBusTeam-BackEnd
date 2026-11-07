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

        builder.UseSnakeCaseNamingConvention();
    }
}
