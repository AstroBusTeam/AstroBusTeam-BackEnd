using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Domain.Repositories;

/// <summary>
///     Repository for routes
/// </summary>
public interface IBusRouteRepository : IBaseRepository<BusRoute>
{
    /// <summary>
    ///     Finds routes, optionally filtered.
    /// </summary>
    Task<IEnumerable<BusRoute>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<BusRoute?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
}
