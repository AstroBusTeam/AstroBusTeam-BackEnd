using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create vehicle resource into a command
/// </summary>
public static class CreateVehicleCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateVehicleCommand ToCommandFromResource(CreateVehicleResource resource)
    {
        return new CreateVehicleCommand(
            new VehicleCode(resource.Code),
            new LicensePlate(resource.Plate),
            resource.Model,
            resource.Capacity,
            new VehicleStatus(resource.Status),
            resource.CurrentLatitude,
            resource.CurrentLongitude);
    }
}
