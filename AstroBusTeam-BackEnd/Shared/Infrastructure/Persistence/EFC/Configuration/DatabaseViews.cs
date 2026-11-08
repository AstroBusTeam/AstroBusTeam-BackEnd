using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Creates the database views that are not mapped to entities.
/// </summary>
public static class DatabaseViews
{
    private const string ShiftSummarySql = """
        CREATE OR REPLACE VIEW v_shift_summary AS
        SELECT s.id AS shift_id,
               s.shift_date,
               s.start_time,
               s.end_time,
               s.status,
               d.id AS driver_id,
               CONCAT(d.first_name, ' ', d.last_name) AS driver_name,
               v.id AS vehicle_id,
               v.code AS vehicle_code,
               v.plate AS vehicle_plate,
               r.id AS route_id,
               r.code AS route_code,
               r.name AS route_name,
               s.distance_km,
               s.passengers,
               s.revenue,
               s.average_speed_kmh,
               s.max_speed_kmh,
               s.overspeed_events,
               TIMESTAMPDIFF(MINUTE, s.start_time, COALESCE(s.end_time, NOW())) AS duration_minutes,
               (SELECT COUNT(*) FROM alerts a WHERE a.shift_id = s.id) AS alert_count
        FROM shifts s
        JOIN drivers d ON d.id = s.driver_id
        JOIN vehicles v ON v.id = s.vehicle_id
        JOIN routes r ON r.id = s.route_id
        """;

    /// <summary>
    ///     Creates or replaces the views of the database.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger used to report a failure without stopping the application.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task CreateAsync(AppDbContext context, ILogger logger,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync(ShiftSummarySql, cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            logger.LogWarning(ex, "The view v_shift_summary could not be created");
        }
    }
}
