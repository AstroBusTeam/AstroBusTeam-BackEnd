using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to update the status and the end date of an assignment
/// </summary>
public record UpdateAssignmentCommand(int Id, AssignmentStatus Status, DateOnly? ValidTo);
