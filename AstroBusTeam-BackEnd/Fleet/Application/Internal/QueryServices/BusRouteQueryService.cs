using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for routes
/// </summary>
public class BusRouteQueryService(IBusRouteRepository repository) : IBusRouteQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<BusRoute>> Handle(GetAllBusRoutesQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<BusRoute?> Handle(GetBusRouteByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
