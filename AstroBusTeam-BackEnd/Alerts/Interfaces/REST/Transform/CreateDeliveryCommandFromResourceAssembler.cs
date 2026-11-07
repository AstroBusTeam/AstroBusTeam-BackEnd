using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

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
