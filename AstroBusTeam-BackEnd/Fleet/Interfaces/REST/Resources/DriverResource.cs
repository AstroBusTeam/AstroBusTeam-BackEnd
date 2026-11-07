namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a driver.
/// </summary>
/// <param name="Id">The server-generated ID of the driver</param>
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
public record DriverResource(
    int Id,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Dni,
    string Category,
    string LicenseNumber,
    DateOnly LicenseExpiry,
    int Points,
    decimal Rating,
    int YearsExperience,
    string Status);
