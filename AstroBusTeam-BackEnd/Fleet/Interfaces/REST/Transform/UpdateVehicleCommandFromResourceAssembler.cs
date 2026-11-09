using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update vehicle resource into a command
/// </summary>
public static class UpdateVehicleCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateVehicleCommand ToCommandFromResource(int id, UpdateVehicleResource resource) =>
        new(id,
            new VehicleCode(resource.Code),
            new LicensePlate(resource.Plate),
            resource.Model,
            resource.Capacity,
            new VehicleStatus(resource.Status),
            resource.CurrentLatitude,
            resource.CurrentLongitude);
}
