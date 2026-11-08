using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a vehicle entity into a resource
/// </summary>
public static class VehicleResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static VehicleResource ToResourceFromEntity(Vehicle entity) =>
        new(entity.Id,
            entity.Code.Value,
            entity.Plate.Value,
            entity.Model,
            entity.Capacity,
            entity.Status.Value,
            entity.CurrentLatitude,
            entity.CurrentLongitude,
            entity.LastPositionAt,
            entity.CreatedAt);
}
