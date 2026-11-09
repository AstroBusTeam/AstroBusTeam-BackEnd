using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update driver resource into a command
/// </summary>
public static class UpdateDriverCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateDriverCommand ToCommandFromResource(int id, UpdateDriverResource resource) =>
        new(id,
            new EmployeeCode(resource.EmployeeCode),
            resource.FirstName,
            resource.LastName,
            new Dni(resource.Dni),
            resource.LicenseCategory,
            new LicenseNumber(resource.LicenseNumber),
            resource.LicenseExpiry,
            resource.Points,
            resource.Rating,
            resource.YearsExperience,
            resource.Phone,
            new DriverStatus(resource.Status));
}
