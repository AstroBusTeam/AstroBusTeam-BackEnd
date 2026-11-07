using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     The Recipient repository contract.
/// </summary>
public interface IRecipientRepository : IBaseRepository<Recipient>
{
    /// <summary>
    ///     Find all recipients, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Recipient>> FindAllAsync(CancellationToken cancellationToken = default);
}
