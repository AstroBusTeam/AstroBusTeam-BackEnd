using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a shift entity into a resource
/// </summary>
public static class ShiftResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static ShiftResource ToResourceFromEntity(Shift entity) =>
        new(entity.Id,
            entity.AssignmentId,
            entity.DriverId,
            entity.VehicleId,
            entity.RouteId,
            entity.ShiftDate,
            entity.StartTime,
            entity.EndTime,
            entity.DistanceKm,
            entity.Passengers,
            entity.Revenue,
            entity.Status.Value,
            entity.AverageSpeedKmh,
            entity.MaxSpeedKmh,
            entity.OverspeedEvents);
}
