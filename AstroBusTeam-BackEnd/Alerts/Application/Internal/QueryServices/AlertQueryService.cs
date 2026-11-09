using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying alerts.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving alerts. All queries delegate to the repository layer.
/// </remarks>
/// <param name="alertRepository">Repository for accessing alert data.</param>
public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken = default) =>
        await alertRepository.FindAllAsync(query.Status, cancellationToken);

    /// <inheritdoc />
    public async Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken = default) =>
        await alertRepository.FindByIdAsync(query.Id, cancellationToken);
}
