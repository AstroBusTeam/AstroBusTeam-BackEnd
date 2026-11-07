using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for alert persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with alert-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class AlertRepository(AppDbContext context)
    : BaseRepository<Alert>(context), IAlertRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> FindAllAsync(AlertStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Alert>().AsQueryable();
        if (status is not null) query = query.Where(a => a.Status == status);
        return await query.OrderBy(a => a.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Alert?> FindByCodeAsync(AlertCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>().FirstOrDefaultAsync(a => a.Code == code, cancellationToken);
    }
}
