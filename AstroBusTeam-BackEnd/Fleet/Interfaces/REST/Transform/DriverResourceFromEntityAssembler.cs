using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a DriverResource from a Driver.
/// </summary>
public static class DriverResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a DriverResource from a Driver.
    /// </summary>
    /// <param name="entity">The Driver entity</param>
    /// <returns>
    ///     A DriverResource assembled from the Driver
    /// </returns>
    public static DriverResource ToResourceFromEntity(Driver entity) =>
        new(entity.Id,
            entity.EmployeeCode.Value,
            entity.FirstName,
            entity.LastName,
            entity.Dni.Value,
            entity.Category,
            entity.LicenseNumber.Value,
            entity.LicenseExpiry,
            entity.Points,
            entity.Rating,
            entity.YearsExperience,
            entity.Status.Value);
}
