using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

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
