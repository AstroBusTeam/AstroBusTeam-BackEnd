using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Command service for routes
/// </summary>
public interface IBusRouteCommandService
{
    /// <summary>
    ///     Handles the creation of a route.
    /// </summary>
    Task<Result<BusRoute, CreateBusRouteError>> Handle(CreateBusRouteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a route.
    /// </summary>
    Task<Result<BusRoute, UpdateBusRouteError>> Handle(UpdateBusRouteCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a route.
    /// </summary>
    Task<Result<BusRoute, DeleteBusRouteError>> Handle(DeleteBusRouteCommand command, CancellationToken cancellationToken = default);
}
