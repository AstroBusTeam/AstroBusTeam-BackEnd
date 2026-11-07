using SecurityBus.API.Fleet.Domain.Model.ValueObjects;

namespace SecurityBus.API.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to delete a assignment
/// </summary>
/// <param name="Id">The identifier of the assignment to delete</param>
public record DeleteAssignmentCommand(int Id);
