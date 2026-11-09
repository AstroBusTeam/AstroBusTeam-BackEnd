using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Operations.Domain.Model.Queries;

/// <summary>
///     Query to get all shifts, optionally filtered
/// </summary>
/// <param name="DriverId">Optional filter by driver id</param>
public record GetAllShiftsQuery(int? DriverId = null);
