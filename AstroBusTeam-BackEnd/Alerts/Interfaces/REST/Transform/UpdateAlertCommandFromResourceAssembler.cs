using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Alerts.Interfaces.REST.Resources;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles an UpdateAlertCommand from an UpdateAlertResource.
/// </summary>
public static class UpdateAlertCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles an UpdateAlertCommand from an UpdateAlertResource.
    /// </summary>
    /// <param name="id">The identifier of the alert taken from the route</param>
    /// <param name="resource">The UpdateAlertResource resource</param>
    /// <returns>
    ///     An UpdateAlertCommand assembled from the UpdateAlertResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static UpdateAlertCommand ToCommandFromResource(int id, UpdateAlertResource resource) =>
        new(id,
            new AlertCode(resource.Code),
            resource.VehicleId,
            resource.Type,
            resource.Receiver,
            resource.Attempts,
            new AlertStatus(resource.Status),
            resource.CreatedAt,
            resource.Latitude,
            resource.Longitude);
}
