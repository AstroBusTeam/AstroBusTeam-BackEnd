using System.Globalization;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an assignment entity into a resource
/// </summary>
public static class AssignmentResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static AssignmentResource ToResourceFromEntity(Assignment entity) =>
        new(entity.Id,
            entity.DriverId,
            entity.VehicleId,
            entity.RouteId,
            entity.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            entity.ValidFrom,
            entity.ValidTo,
            entity.Status.Value,
            entity.AssignedBy);
}
