using SecurityBus.API.Fleet.Domain.Model.ValueObjects;

namespace SecurityBus.API.Fleet.Domain.Model.Queries;

/// <summary>
///     Query to get all assignments, optionally filtered
/// </summary>
/// <param name="DriverId">Optional filter by driver id</param>
/// <param name="Status">Optional filter by status</param>
public record GetAllAssignmentsQuery(int? DriverId = null, AssignmentStatus? Status = null);
