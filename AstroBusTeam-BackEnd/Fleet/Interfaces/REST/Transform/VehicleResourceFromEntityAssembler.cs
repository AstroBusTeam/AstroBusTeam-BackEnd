using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

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
