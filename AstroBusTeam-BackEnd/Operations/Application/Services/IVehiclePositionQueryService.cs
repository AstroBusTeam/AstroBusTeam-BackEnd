using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Operations.Application.Services;

/// <summary>
///     Query service for vehicle positions
/// </summary>
public interface IVehiclePositionQueryService
{
    /// <summary>
    ///     Handles the listing of vehicle positions.
    /// </summary>
    Task<IEnumerable<VehiclePosition>> Handle(GetAllVehiclePositionsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a vehicle position by identifier.
    /// </summary>
    Task<VehiclePosition?> Handle(GetVehiclePositionByIdQuery query, CancellationToken cancellationToken = default);
}
