using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Repositories;

namespace SecurityBus.API.Alerts.Domain.Repositories;

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
