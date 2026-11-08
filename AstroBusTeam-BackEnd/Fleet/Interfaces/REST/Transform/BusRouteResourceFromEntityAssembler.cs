using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a route entity into a resource
/// </summary>
public static class BusRouteResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static BusRouteResource ToResourceFromEntity(BusRoute entity) =>
        new(entity.Id,
            entity.Code,
            entity.Name,
            entity.Origin,
            entity.Destination,
            entity.DistanceKm,
            entity.Active);
}
