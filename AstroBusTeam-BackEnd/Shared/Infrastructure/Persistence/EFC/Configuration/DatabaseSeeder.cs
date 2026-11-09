using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Loads the sample data into the empty tables
/// </summary>
/// <remarks>
///     The tables are filled in dependency order, so every foreign key points to a row that already exists.
/// </remarks>
public static class DatabaseSeeder
{
    /// <summary>
    ///     Seeds every empty table with its sample data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Set<ControlCenter>().AnyAsync(cancellationToken))
        {
            await context.Set<ControlCenter>().AddRangeAsync(SampleData.ControlCenters(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Driver>().AnyAsync(cancellationToken))
        {
            await context.Set<Driver>().AddRangeAsync(SampleData.Drivers(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Vehicle>().AnyAsync(cancellationToken))
        {
            await context.Set<Vehicle>().AddRangeAsync(SampleData.Vehicles(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<BusRoute>().AnyAsync(cancellationToken))
        {
            await context.Set<BusRoute>().AddRangeAsync(SampleData.BusRoutes(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<User>().AnyAsync(cancellationToken))
        {
            await context.Set<User>().AddRangeAsync(SampleData.Users(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Assignment>().AnyAsync(cancellationToken))
        {
            await context.Set<Assignment>().AddRangeAsync(SampleData.Assignments(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Shift>().AnyAsync(cancellationToken))
        {
            await context.Set<Shift>().AddRangeAsync(SampleData.Shifts(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<VehiclePosition>().AnyAsync(cancellationToken))
        {
            await context.Set<VehiclePosition>().AddRangeAsync(SampleData.VehiclePositions(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<AlertType>().AnyAsync(cancellationToken))
        {
            await context.Set<AlertType>().AddRangeAsync(SampleData.AlertTypes(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<NotificationChannel>().AnyAsync(cancellationToken))
        {
            await context.Set<NotificationChannel>().AddRangeAsync(SampleData.NotificationChannels(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Recipient>().AnyAsync(cancellationToken))
        {
            await context.Set<Recipient>().AddRangeAsync(SampleData.Recipients(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Alert>().AnyAsync(cancellationToken))
        {
            await context.Set<Alert>().AddRangeAsync(SampleData.Alerts(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<AlertAttempt>().AnyAsync(cancellationToken))
        {
            await context.Set<AlertAttempt>().AddRangeAsync(SampleData.AlertAttempts(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Delivery>().AnyAsync(cancellationToken))
        {
            await context.Set<Delivery>().AddRangeAsync(SampleData.Deliveries(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
