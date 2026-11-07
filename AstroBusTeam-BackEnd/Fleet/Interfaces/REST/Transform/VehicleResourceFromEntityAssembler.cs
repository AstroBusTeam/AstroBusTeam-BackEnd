using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a VehicleResource from a Vehicle.
/// </summary>
public static class VehicleResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a VehicleResource from a Vehicle.
    /// </summary>
    /// <param name="entity">The Vehicle entity</param>
    /// <returns>
    ///     A VehicleResource assembled from the Vehicle
    /// </returns>
    public static VehicleResource ToResourceFromEntity(Vehicle entity) =>
        new(entity.Id,
            entity.Code.Value,
            entity.Plate.Value,
            entity.Model,
            entity.Capacity,
            entity.Status.Value,
            entity.Latitude,
            entity.Longitude);
}
