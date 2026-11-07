using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an UpdateDriverCommand from an UpdateDriverResource.
/// </summary>
public static class UpdateDriverCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles an UpdateDriverCommand from an UpdateDriverResource.
    /// </summary>
    /// <param name="id">The identifier of the driver taken from the route</param>
    /// <param name="resource">The UpdateDriverResource resource</param>
    /// <returns>
    ///     An UpdateDriverCommand assembled from the UpdateDriverResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static UpdateDriverCommand ToCommandFromResource(int id, UpdateDriverResource resource) =>
        new(id,
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
