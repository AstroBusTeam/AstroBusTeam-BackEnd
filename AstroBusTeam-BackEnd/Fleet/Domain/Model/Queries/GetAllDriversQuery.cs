using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

/// <summary>
///     Query to get all drivers, optionally filtered
/// </summary>
/// <param name="EmployeeCode">Optional filter by employee code</param>
public record GetAllDriversQuery(EmployeeCode? EmployeeCode = null);
