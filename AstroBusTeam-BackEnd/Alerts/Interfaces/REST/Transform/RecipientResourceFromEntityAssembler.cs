using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a recipient entity into a resource
/// </summary>
public static class RecipientResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static RecipientResource ToResourceFromEntity(Recipient entity) =>
        new(entity.Id,
            entity.Name,
            entity.Role,
            entity.Email,
            entity.Phone,
            entity.ControlCenterId,
            entity.Active,
            entity.Channels.Select(link => link.ChannelId).OrderBy(id => id).ToList());
}
