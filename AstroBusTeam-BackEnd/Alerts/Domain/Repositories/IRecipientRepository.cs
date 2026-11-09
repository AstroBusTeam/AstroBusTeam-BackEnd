using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     Repository for recipients
/// </summary>
public interface IRecipientRepository : IBaseRepository<Recipient>
{
    /// <summary>
    ///     Finds all recipients with their notification channels.
    /// </summary>
    Task<IEnumerable<Recipient>> FindAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a recipient by identifier, including its notification channels.
    /// </summary>
    Task<Recipient?> FindByIdWithChannelsAsync(int id, CancellationToken cancellationToken = default);
}
