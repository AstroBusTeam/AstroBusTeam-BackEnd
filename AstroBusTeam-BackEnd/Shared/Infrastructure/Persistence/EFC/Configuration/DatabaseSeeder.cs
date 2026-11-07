using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Inserts the sample data into an empty database.
/// </summary>
/// <remarks>
///     Each table is seeded only when it is empty, so the seeder is safe to run on every startup. Tables are
///     seeded one at a time, in dependency order, so the generated identifiers match the Fake API (1, 2, 3...).
/// </remarks>
public static class DatabaseSeeder
{
    /// <summary>
    ///     Seeds every empty table with the sample data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
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

        if (!await context.Set<Alert>().AnyAsync(cancellationToken))
        {
            await context.Set<Alert>().AddRangeAsync(SampleData.Alerts(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Recipient>().AnyAsync(cancellationToken))
        {
            await context.Set<Recipient>().AddRangeAsync(SampleData.Recipients(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Set<Delivery>().AnyAsync(cancellationToken))
        {
            await context.Set<Delivery>().AddRangeAsync(SampleData.Deliveries(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
