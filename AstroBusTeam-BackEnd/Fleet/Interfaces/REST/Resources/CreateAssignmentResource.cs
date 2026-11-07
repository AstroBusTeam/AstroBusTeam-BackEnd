using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to create a assignment.
/// </summary>
/// <param name="DriverId">Driver ID</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Route">Route</param>
/// <param name="StartTime">Start time (HH:mm)</param>
/// <param name="Status">Assignment status (active or inactive)</param>
public record CreateAssignmentResource(
    int DriverId,
    int VehicleId,
    [Required(ErrorMessage = "RequiredAssignmentRoute")]
    string Route,
    [Required(ErrorMessage = "RequiredAssignmentStartTime")]
    string StartTime,
    [Required(ErrorMessage = "RequiredAssignmentStatus")]
    string Status);
