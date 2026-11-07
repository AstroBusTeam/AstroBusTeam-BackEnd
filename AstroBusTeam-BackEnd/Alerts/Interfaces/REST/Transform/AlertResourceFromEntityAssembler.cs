using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles a AlertResource from a Alert.
/// </summary>
public static class AlertResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a AlertResource from a Alert.
    /// </summary>
    /// <param name="entity">The Alert entity</param>
    /// <returns>
    ///     A AlertResource assembled from the Alert
    /// </returns>
    public static AlertResource ToResourceFromEntity(Alert entity) =>
        new(entity.Id,
            entity.Code.Value,
            entity.VehicleId,
            entity.Type,
            entity.Receiver,
            entity.Attempts,
            entity.Status.Value,
            entity.OccurredAt,
            entity.Latitude,
            entity.Longitude);
}
