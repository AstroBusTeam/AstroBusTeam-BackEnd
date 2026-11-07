using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles a RecipientResource from a Recipient.
/// </summary>
public static class RecipientResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a RecipientResource from a Recipient.
    /// </summary>
    /// <param name="entity">The Recipient entity</param>
    /// <returns>
    ///     A RecipientResource assembled from the Recipient
    /// </returns>
    public static RecipientResource ToResourceFromEntity(Recipient entity) =>
        new(entity.Id,
            entity.Name,
            entity.Role,
            entity.Channel,
            entity.Active);
}
