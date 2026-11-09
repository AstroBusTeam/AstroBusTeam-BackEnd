using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a delivery entity into a resource
/// </summary>
public static class DeliveryResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static DeliveryResource ToResourceFromEntity(Delivery entity) =>
        new(entity.Id,
            entity.AlertId,
            entity.RecipientId,
            entity.ChannelId,
            entity.AlertTypeId,
            entity.Priority,
            entity.Status.Value,
            entity.SentAt,
            entity.ErrorMessage);
}
