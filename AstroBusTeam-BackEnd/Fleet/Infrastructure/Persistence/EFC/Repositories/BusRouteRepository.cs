using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for routes
/// </summary>
public class BusRouteRepository(AppDbContext context)
    : BaseRepository<BusRoute>(context), IBusRouteRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<BusRoute>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<BusRoute>().AsQueryable();
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BusRoute?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<BusRoute>().FirstOrDefaultAsync(e => e.Code == code, cancellationToken);
    }
}
