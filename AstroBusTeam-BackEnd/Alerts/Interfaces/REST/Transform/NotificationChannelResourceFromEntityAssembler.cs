using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a notification channel entity into a resource
/// </summary>
public static class NotificationChannelResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static NotificationChannelResource ToResourceFromEntity(NotificationChannel entity) =>
        new(entity.Id,
            entity.Code);
}
