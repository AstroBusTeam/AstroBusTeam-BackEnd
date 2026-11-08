using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a alert type entity into a resource
/// </summary>
public static class AlertTypeResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static AlertTypeResource ToResourceFromEntity(AlertType entity) =>
        new(entity.Id,
            entity.Name,
            entity.DefaultPriority,
            entity.IsPanic);
}
