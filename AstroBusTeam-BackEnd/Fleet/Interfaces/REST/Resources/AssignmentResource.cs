namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a assignment.
/// </summary>
/// <param name="Id">The server-generated ID of the assignment</param>
/// <param name="DriverId">Driver ID</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Route">Route</param>
/// <param name="StartTime">Start time (HH:mm)</param>
/// <param name="Status">Assignment status (active or inactive)</param>
public record AssignmentResource(
    int Id,
    int DriverId,
    int VehicleId,
    string Route,
    string StartTime,
    string Status);
