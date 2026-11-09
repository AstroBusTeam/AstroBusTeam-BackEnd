using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for control centers
/// </summary>
public class ControlCenterRepository(AppDbContext context)
    : BaseRepository<ControlCenter>(context), IControlCenterRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<ControlCenter>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<ControlCenter>().AsQueryable();
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ControlCenter?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ControlCenter>().FirstOrDefaultAsync(e => e.Code == code, cancellationToken);
    }
}
