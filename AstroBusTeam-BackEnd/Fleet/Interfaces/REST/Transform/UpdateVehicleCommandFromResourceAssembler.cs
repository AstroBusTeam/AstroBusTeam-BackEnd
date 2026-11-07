using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an UpdateVehicleCommand from an UpdateVehicleResource.
/// </summary>
public static class UpdateVehicleCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles an UpdateVehicleCommand from an UpdateVehicleResource.
    /// </summary>
    /// <param name="id">The identifier of the vehicle taken from the route</param>
    /// <param name="resource">The UpdateVehicleResource resource</param>
    /// <returns>
    ///     An UpdateVehicleCommand assembled from the UpdateVehicleResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static UpdateVehicleCommand ToCommandFromResource(int id, UpdateVehicleResource resource) =>
        new(id,
            new VehicleCode(resource.Code),
            new LicensePlate(resource.Plate),
            resource.Model,
            resource.Capacity,
            new VehicleStatus(resource.Status),
            resource.Latitude,
            resource.Longitude);
}
