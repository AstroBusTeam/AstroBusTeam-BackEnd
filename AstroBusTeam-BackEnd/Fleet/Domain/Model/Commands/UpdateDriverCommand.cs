using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to update a driver
/// </summary>
public record UpdateDriverCommand(int Id, EmployeeCode EmployeeCode, string FirstName, string LastName, Dni Dni, string LicenseCategory, LicenseNumber LicenseNumber, DateOnly LicenseExpiry, int Points, decimal Rating, int YearsExperience, string? Phone, DriverStatus Status);
