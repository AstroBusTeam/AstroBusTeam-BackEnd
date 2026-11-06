using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Alerts.Interfaces.REST.Resources;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateDeliveryCommand from a CreateDeliveryResource.
/// </summary>
public static class CreateDeliveryCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateDeliveryCommand from a CreateDeliveryResource.
    /// </summary>
    /// <param name="resource">The CreateDeliveryResource resource</param>
    /// <returns>
    ///     A CreateDeliveryCommand assembled from the CreateDeliveryResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateDeliveryCommand ToCommandFromResource(CreateDeliveryResource resource)
    {
        return new CreateDeliveryCommand(
            resource.RecipientName,
            resource.Channel,
            resource.AlertType,
            resource.Priority,
            new DeliveryStatus(resource.Status ?? "sent"),
            resource.SentAt ?? LocalClock.Now());
    }
}
