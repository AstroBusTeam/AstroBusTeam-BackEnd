using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an alert entity into a resource
/// </summary>
public static class AlertResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static AlertResource ToResourceFromEntity(Alert entity) =>
        new(entity.Id,
            entity.Code.Value,
            entity.VehicleId,
            entity.DriverId,
            entity.ShiftId,
            entity.AlertTypeId,
            entity.ReceiverId,
            entity.Attempts,
            entity.Status.Value,
            entity.CreatedAt,
            entity.Latitude,
            entity.Longitude,
            entity.ConfirmedAt,
            entity.ConfirmedBy,
            entity.Notes);
}
