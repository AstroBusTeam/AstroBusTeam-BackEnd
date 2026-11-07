using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles an UpdateRecipientCommand from an UpdateRecipientResource.
/// </summary>
public static class UpdateRecipientCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles an UpdateRecipientCommand from an UpdateRecipientResource.
    /// </summary>
    /// <param name="id">The identifier of the recipient taken from the route</param>
    /// <param name="resource">The UpdateRecipientResource resource</param>
    /// <returns>
    ///     An UpdateRecipientCommand assembled from the UpdateRecipientResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static UpdateRecipientCommand ToCommandFromResource(int id, UpdateRecipientResource resource) =>
        new(id,
            resource.Name,
            resource.Role,
            resource.Channel,
            resource.Active);
}
