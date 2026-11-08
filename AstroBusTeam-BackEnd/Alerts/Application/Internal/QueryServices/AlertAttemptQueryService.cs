using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for alert attempts
/// </summary>
public class AlertAttemptQueryService(IAlertAttemptRepository repository) : IAlertAttemptQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AlertAttempt>> Handle(GetAllAlertAttemptsQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(query.AlertId, cancellationToken);

    /// <inheritdoc />
    public async Task<AlertAttempt?> Handle(GetAlertAttemptByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
