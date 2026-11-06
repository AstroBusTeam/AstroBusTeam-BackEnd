using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Interfaces.REST.Resources;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

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
