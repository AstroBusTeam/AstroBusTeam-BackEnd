using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for alert types
/// </summary>
public class AlertTypeRepository(AppDbContext context)
    : BaseRepository<AlertType>(context), IAlertTypeRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<AlertType>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<AlertType>().AsQueryable();
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AlertType?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await Context.Set<AlertType>().FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }
}
