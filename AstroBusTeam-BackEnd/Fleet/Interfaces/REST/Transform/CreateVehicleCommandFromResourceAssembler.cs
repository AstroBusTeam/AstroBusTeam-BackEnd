using SecurityBus.API.Fleet.Domain.Model.Commands;
using SecurityBus.API.Fleet.Domain.Model.ValueObjects;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateVehicleCommand from a CreateVehicleResource.
/// </summary>
public static class CreateVehicleCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateVehicleCommand from a CreateVehicleResource.
    /// </summary>
    /// <param name="resource">The CreateVehicleResource resource</param>
    /// <returns>
    ///     A CreateVehicleCommand assembled from the CreateVehicleResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateVehicleCommand ToCommandFromResource(CreateVehicleResource resource)
    {
        return new CreateVehicleCommand(
            new VehicleCode(resource.Code),
            new LicensePlate(resource.Plate),
            resource.Model,
            resource.Capacity,
            new VehicleStatus(resource.Status),
            resource.Latitude,
            resource.Longitude);
    }
}
