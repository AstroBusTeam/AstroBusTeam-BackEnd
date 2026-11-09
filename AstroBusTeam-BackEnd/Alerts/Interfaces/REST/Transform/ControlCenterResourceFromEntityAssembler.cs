using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a control center entity into a resource
/// </summary>
public static class ControlCenterResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static ControlCenterResource ToResourceFromEntity(ControlCenter entity) =>
        new(entity.Id,
            entity.Code,
            entity.Name,
            entity.Zone,
            entity.Active);
}
