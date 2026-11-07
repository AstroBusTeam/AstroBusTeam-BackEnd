using SecurityBus.API.Fleet.Domain.Model.Commands;
using SecurityBus.API.Fleet.Domain.Model.ValueObjects;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateDriverCommand from a CreateDriverResource.
/// </summary>
public static class CreateDriverCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateDriverCommand from a CreateDriverResource.
    /// </summary>
    /// <param name="resource">The CreateDriverResource resource</param>
    /// <returns>
    ///     A CreateDriverCommand assembled from the CreateDriverResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateDriverCommand ToCommandFromResource(CreateDriverResource resource)
    {
        return new CreateDriverCommand(
            new EmployeeCode(resource.EmployeeCode),
            resource.FirstName,
            resource.LastName,
            new Dni(resource.Dni),
            resource.Category,
            new LicenseNumber(resource.LicenseNumber),
            resource.LicenseExpiry,
            resource.Points,
            resource.Rating,
            resource.YearsExperience,
            new DriverStatus(resource.Status));
    }
}
