using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to update the status and the end date of an assignment
/// </summary>
/// <remarks>
///     Use <c>inactive</c> to release the driver and the vehicle without deleting the assignment.
/// </remarks>
public record UpdateAssignmentResource(
    [Required(ErrorMessage = "RequiredAssignmentStatus")]
    string Status,
    DateOnly? ValidTo = null);
