using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service for alert attempts
/// </summary>
public interface IAlertAttemptQueryService
{
    /// <summary>
    ///     Handles the listing of alert attempts.
    /// </summary>
    Task<IEnumerable<AlertAttempt>> Handle(GetAllAlertAttemptsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a alert attempt by identifier.
    /// </summary>
    Task<AlertAttempt?> Handle(GetAlertAttemptByIdQuery query, CancellationToken cancellationToken = default);
}
