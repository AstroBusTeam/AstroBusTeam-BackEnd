using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for alert types
/// </summary>
public class AlertTypeQueryService(IAlertTypeRepository repository) : IAlertTypeQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AlertType>> Handle(GetAllAlertTypesQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<AlertType?> Handle(GetAlertTypeByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
