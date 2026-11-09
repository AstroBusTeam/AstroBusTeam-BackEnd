using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to delete a assignment
/// </summary>
/// <param name="Id">The identifier of the assignment to delete</param>
public record DeleteAssignmentCommand(int Id);
