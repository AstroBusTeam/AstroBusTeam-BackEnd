using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateAlertCommand from a CreateAlertResource.
/// </summary>
public static class CreateAlertCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateAlertCommand from a CreateAlertResource.
    /// </summary>
    /// <param name="resource">The CreateAlertResource resource</param>
    /// <returns>
    ///     A CreateAlertCommand assembled from the CreateAlertResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
    {
        return new CreateAlertCommand(
            new AlertCode(resource.Code ?? AlertCode.Generate().Value),
            resource.VehicleId,
            resource.Type,
            resource.Receiver,
            resource.Attempts ?? 1,
            new AlertStatus(resource.Status ?? "pending"),
            resource.CreatedAt ?? LocalClock.Now(),
            resource.Latitude,
            resource.Longitude);
    }
}
