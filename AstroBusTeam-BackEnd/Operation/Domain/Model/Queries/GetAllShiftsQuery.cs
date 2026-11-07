using SecurityBus.API.Operations.Domain.Model.ValueObjects;

namespace SecurityBus.API.Operations.Domain.Model.Queries;

/// <summary>
///     Query to get all shifts, optionally filtered
/// </summary>
/// <param name="DriverId">Optional filter by driver id</param>
public record GetAllShiftsQuery(int? DriverId = null);
