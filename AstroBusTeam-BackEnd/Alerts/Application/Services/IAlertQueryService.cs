using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service interface for alert operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying alerts. It supports listing (with optional filters) and
///     retrieving a single alert by its identifier.
/// </remarks>
public interface IAlertQueryService
{
    /// <summary>
    ///     Handle the GetAllAlertsQuery.
    /// </summary>
    /// <param name="query">The GetAllAlertsQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Alert objects</returns>
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetAlertByIdQuery.
    /// </summary>
    /// <param name="query">The GetAlertByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Alert object if found, or null otherwise</returns>
    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken = default);
}
