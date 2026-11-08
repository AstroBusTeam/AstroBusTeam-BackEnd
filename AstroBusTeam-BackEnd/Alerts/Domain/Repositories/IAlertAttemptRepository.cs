using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     Repository for alert attempts
/// </summary>
public interface IAlertAttemptRepository : IBaseRepository<AlertAttempt>
{
    /// <summary>
    ///     Finds alert attempts, optionally filtered.
    /// </summary>
    Task<IEnumerable<AlertAttempt>> FindAllAsync(int? alertId = null, CancellationToken cancellationToken = default);
}
