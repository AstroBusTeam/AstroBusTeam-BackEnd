using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update route resource into a command
/// </summary>
public static class UpdateBusRouteCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateBusRouteCommand ToCommandFromResource(int id, UpdateBusRouteResource resource) =>
        new UpdateBusRouteCommand(
            id,
            resource.Code,
            resource.Name,
            resource.Origin,
            resource.Destination,
            resource.DistanceKm,
            resource.Active);
}
