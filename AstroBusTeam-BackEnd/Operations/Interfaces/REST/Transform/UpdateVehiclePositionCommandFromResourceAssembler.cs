using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update vehicle position resource into a command
/// </summary>
public static class UpdateVehiclePositionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateVehiclePositionCommand ToCommandFromResource(int id, UpdateVehiclePositionResource resource) =>
        new UpdateVehiclePositionCommand(
            id,
            resource.VehicleId,
            resource.ShiftId,
            resource.Latitude,
            resource.Longitude,
            resource.SpeedKmh,
            resource.RecordedAt);
}
