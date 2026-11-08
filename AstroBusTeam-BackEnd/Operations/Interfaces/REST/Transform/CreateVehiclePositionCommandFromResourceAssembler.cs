using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create vehicle position resource into a command
/// </summary>
public static class CreateVehiclePositionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateVehiclePositionCommand ToCommandFromResource(CreateVehiclePositionResource resource) =>
        new CreateVehiclePositionCommand(
            resource.VehicleId,
            resource.ShiftId,
            resource.Latitude,
            resource.Longitude,
            resource.SpeedKmh ?? 0.0,
            resource.RecordedAt ?? LocalClock.Now());
}
