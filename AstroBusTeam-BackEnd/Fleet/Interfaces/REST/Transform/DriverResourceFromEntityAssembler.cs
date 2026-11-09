using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a driver entity into a resource
/// </summary>
public static class DriverResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static DriverResource ToResourceFromEntity(Driver entity) =>
        new(entity.Id,
            entity.EmployeeCode.Value,
            entity.FirstName,
            entity.LastName,
            entity.Dni.Value,
            entity.LicenseCategory,
            entity.LicenseNumber.Value,
            entity.LicenseExpiry,
            entity.Points,
            entity.Rating,
            entity.YearsExperience,
            entity.Phone,
            entity.Status.Value,
            entity.CreatedAt);
}
