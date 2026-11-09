using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a alert attempt entity into a resource
/// </summary>
public static class AlertAttemptResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static AlertAttemptResource ToResourceFromEntity(AlertAttempt entity) =>
        new(entity.Id,
            entity.AlertId,
            entity.AttemptNumber,
            entity.SentAt,
            entity.Result);
}
