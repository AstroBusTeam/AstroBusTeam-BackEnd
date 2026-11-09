using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update assignment resource into a command
/// </summary>
public static class UpdateAssignmentCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateAssignmentCommand ToCommandFromResource(int id, UpdateAssignmentResource resource) =>
        new(id, new AssignmentStatus(resource.Status), resource.ValidTo);
}
