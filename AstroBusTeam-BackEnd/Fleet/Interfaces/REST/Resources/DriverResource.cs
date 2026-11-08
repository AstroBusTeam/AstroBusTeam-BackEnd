namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a driver
/// </summary>
public record DriverResource(
    int Id,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Dni,
    string LicenseCategory,
    string LicenseNumber,
    DateOnly LicenseExpiry,
    int Points,
    decimal Rating,
    int YearsExperience,
    string? Phone,
    string Status,
    DateTimeOffset? CreatedAt);
