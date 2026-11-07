using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to create a assignment
/// </summary>
/// <param name="DriverId">Driver ID</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Route">Route</param>
/// <param name="StartTime">Start time (HH:mm)</param>
/// <param name="Status">Assignment status (active or inactive)</param>
public record CreateAssignmentCommand(int DriverId, int VehicleId, string Route, TimeOnly StartTime, AssignmentStatus Status);
