using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for alert attempts
/// </summary>
public class AlertAttemptRepository(AppDbContext context)
    : BaseRepository<AlertAttempt>(context), IAlertAttemptRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<AlertAttempt>> FindAllAsync(int? alertId = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<AlertAttempt>().AsQueryable();
        if (alertId is not null) query = query.Where(e => e.AlertId == alertId.Value);
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }
}
