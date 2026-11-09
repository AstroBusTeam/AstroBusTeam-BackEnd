using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a driver
/// </summary>
public record UpdateDriverResource(
    [Required(ErrorMessage = "RequiredDriverEmployeeCode")]
    string EmployeeCode,
    [Required(ErrorMessage = "RequiredDriverFirstName")]
    string FirstName,
    [Required(ErrorMessage = "RequiredDriverLastName")]
    string LastName,
    [Required(ErrorMessage = "RequiredDriverDni")]
    string Dni,
    [Required(ErrorMessage = "RequiredDriverLicenseCategory")]
    string LicenseCategory,
    [Required(ErrorMessage = "RequiredDriverLicenseNumber")]
    string LicenseNumber,
    DateOnly LicenseExpiry,
    int Points,
    decimal Rating,
    int YearsExperience,
    [Required(ErrorMessage = "RequiredDriverStatus")]
    string Status,
    string? Phone = null);
