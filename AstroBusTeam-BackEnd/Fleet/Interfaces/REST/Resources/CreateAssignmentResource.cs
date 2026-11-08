using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to create an assignment
/// </summary>
public record CreateAssignmentResource(
    int DriverId,
    int VehicleId,
    int RouteId,
    [Required(ErrorMessage = "RequiredAssignmentStartTime")]
    string StartTime,
    [Required(ErrorMessage = "RequiredAssignmentStatus")]
    string Status,
    DateOnly? ValidFrom = null,
    DateOnly? ValidTo = null,
    int? AssignedBy = null);
