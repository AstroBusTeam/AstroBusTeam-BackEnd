using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update alert attempt resource into a command
/// </summary>
public static class UpdateAlertAttemptCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateAlertAttemptCommand ToCommandFromResource(int id, UpdateAlertAttemptResource resource) =>
        new UpdateAlertAttemptCommand(
            id,
            resource.AlertId,
            resource.AttemptNumber,
            resource.SentAt,
            resource.Result);
}
