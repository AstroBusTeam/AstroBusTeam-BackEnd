using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create delivery resource into a command
/// </summary>
public static class CreateDeliveryCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateDeliveryCommand ToCommandFromResource(CreateDeliveryResource resource)
    {
        return new CreateDeliveryCommand(
            resource.AlertId,
            resource.RecipientId,
            resource.ChannelId,
            resource.AlertTypeId,
            resource.Priority,
            new DeliveryStatus(resource.Status ?? "sent"),
            resource.SentAt ?? LocalClock.Now(),
            resource.ErrorMessage);
    }
}
