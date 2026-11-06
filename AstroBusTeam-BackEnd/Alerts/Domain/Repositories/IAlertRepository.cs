using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Repositories;

namespace SecurityBus.API.Alerts.Domain.Repositories;

/// <summary>
///     The Alert repository contract.
/// </summary>
public interface IAlertRepository : IBaseRepository<Alert>
{
    /// <summary>
    ///     Find all alerts, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Alert>> FindAllAsync(AlertStatus? status = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find an alert by code.
    /// </summary>
    Task<Alert?> FindByCodeAsync(AlertCode code, CancellationToken cancellationToken = default);
}
