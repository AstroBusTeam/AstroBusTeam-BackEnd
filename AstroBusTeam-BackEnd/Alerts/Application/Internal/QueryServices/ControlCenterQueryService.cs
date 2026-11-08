using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for control centers
/// </summary>
public class ControlCenterQueryService(IControlCenterRepository repository) : IControlCenterQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ControlCenter>> Handle(GetAllControlCentersQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<ControlCenter?> Handle(GetControlCenterByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
