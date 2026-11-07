using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles a DeliveryResource from a Delivery.
/// </summary>
public static class DeliveryResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a DeliveryResource from a Delivery.
    /// </summary>
    /// <param name="entity">The Delivery entity</param>
    /// <returns>
    ///     A DeliveryResource assembled from the Delivery
    /// </returns>
    public static DeliveryResource ToResourceFromEntity(Delivery entity) =>
        new(entity.Id,
            entity.RecipientName,
            entity.Channel,
            entity.AlertType,
            entity.Priority,
            entity.Status.Value,
            entity.SentAt);
}
