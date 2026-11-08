using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create route resource into a command
/// </summary>
public static class CreateBusRouteCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateBusRouteCommand ToCommandFromResource(CreateBusRouteResource resource) =>
        new CreateBusRouteCommand(
            resource.Code,
            resource.Name,
            resource.Origin,
            resource.Destination,
            resource.DistanceKm ?? 0m,
            resource.Active ?? true);
}
