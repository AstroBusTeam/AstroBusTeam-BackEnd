using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a vehicle position entity into a resource
/// </summary>
public static class VehiclePositionResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static VehiclePositionResource ToResourceFromEntity(VehiclePosition entity) =>
        new(entity.Id,
            entity.VehicleId,
            entity.ShiftId,
            entity.Latitude,
            entity.Longitude,
            entity.SpeedKmh,
            entity.RecordedAt);
}
