using System.Globalization;
using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a AssignmentResource from a Assignment.
/// </summary>
public static class AssignmentResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a AssignmentResource from a Assignment.
    /// </summary>
    /// <param name="entity">The Assignment entity</param>
    /// <returns>
    ///     A AssignmentResource assembled from the Assignment
    /// </returns>
    public static AssignmentResource ToResourceFromEntity(Assignment entity) =>
        new(entity.Id,
            entity.DriverId,
            entity.VehicleId,
            entity.Route,
            entity.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            entity.Status.Value);
}
