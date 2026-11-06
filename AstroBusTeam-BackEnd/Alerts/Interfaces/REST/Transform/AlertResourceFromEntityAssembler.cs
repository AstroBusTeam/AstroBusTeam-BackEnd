using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Interfaces.REST.Resources;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

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
