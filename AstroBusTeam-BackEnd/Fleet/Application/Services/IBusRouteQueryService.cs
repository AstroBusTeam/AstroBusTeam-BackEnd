using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Query service for routes
/// </summary>
public interface IBusRouteQueryService
{
    /// <summary>
    ///     Handles the listing of routes.
    /// </summary>
    Task<IEnumerable<BusRoute>> Handle(GetAllBusRoutesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a route by identifier.
    /// </summary>
    Task<BusRoute?> Handle(GetBusRouteByIdQuery query, CancellationToken cancellationToken = default);
}
