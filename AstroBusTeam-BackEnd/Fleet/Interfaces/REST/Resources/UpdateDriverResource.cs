using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to update a driver. The identifier is taken from the route.
/// </summary>
/// <param name="EmployeeCode">Employee code</param>
/// <param name="FirstName">First name</param>
/// <param name="LastName">Last name</param>
/// <param name="Dni">National ID (DNI)</param>
/// <param name="Category">License category</param>
/// <param name="LicenseNumber">License number</param>
/// <param name="LicenseExpiry">License expiry date</param>
/// <param name="Points">License points</param>
/// <param name="Rating">Rating from 0 to 5</param>
/// <param name="YearsExperience">Years of experience</param>
/// <param name="Status">Driver status (active or inactive)</param>
public record UpdateDriverResource(
    [Required(ErrorMessage = "RequiredDriverEmployeeCode")]
    string EmployeeCode,
    [Required(ErrorMessage = "RequiredDriverFirstName")]
    string FirstName,
    [Required(ErrorMessage = "RequiredDriverLastName")]
    string LastName,
    [Required(ErrorMessage = "RequiredDriverDni")]
    string Dni,
    [Required(ErrorMessage = "RequiredDriverCategory")]
    string Category,
    [Required(ErrorMessage = "RequiredDriverLicenseNumber")]
    string LicenseNumber,
    DateOnly LicenseExpiry,
    int Points,
    decimal Rating,
    int YearsExperience,
    [Required(ErrorMessage = "RequiredDriverStatus")]
    string Status);
