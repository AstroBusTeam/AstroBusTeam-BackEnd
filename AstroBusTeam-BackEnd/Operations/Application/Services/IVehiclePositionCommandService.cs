using AstroBusTeamBackEnd.Operations.Application.Errors;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Operations.Application.Services;

/// <summary>
///     Command service for vehicle positions
/// </summary>
public interface IVehiclePositionCommandService
{
    /// <summary>
    ///     Handles the creation of a vehicle position.
    /// </summary>
    Task<Result<VehiclePosition, CreateVehiclePositionError>> Handle(CreateVehiclePositionCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a vehicle position.
    /// </summary>
    Task<Result<VehiclePosition, UpdateVehiclePositionError>> Handle(UpdateVehiclePositionCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a vehicle position.
    /// </summary>
    Task<Result<VehiclePosition, DeleteVehiclePositionError>> Handle(DeleteVehiclePositionCommand command, CancellationToken cancellationToken = default);
}
