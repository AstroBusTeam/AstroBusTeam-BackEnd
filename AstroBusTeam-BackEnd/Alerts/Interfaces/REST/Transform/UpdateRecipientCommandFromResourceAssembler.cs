using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update recipient resource into a command
/// </summary>
public static class UpdateRecipientCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateRecipientCommand ToCommandFromResource(int id, UpdateRecipientResource resource) =>
        new(id,
            resource.Name,
            resource.Role,
            resource.Email,
            resource.Phone,
            resource.ControlCenterId,
            resource.Active,
            resource.ChannelIds);
}
