using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create alert attempt resource into a command
/// </summary>
public static class CreateAlertAttemptCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateAlertAttemptCommand ToCommandFromResource(CreateAlertAttemptResource resource) =>
        new CreateAlertAttemptCommand(
            resource.AlertId,
            resource.AttemptNumber,
            resource.SentAt ?? LocalClock.Now(),
            resource.Result ?? "success");
}
