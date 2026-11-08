using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service for control centers
/// </summary>
public interface IControlCenterQueryService
{
    /// <summary>
    ///     Handles the listing of control centers.
    /// </summary>
    Task<IEnumerable<ControlCenter>> Handle(GetAllControlCentersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a control center by identifier.
    /// </summary>
    Task<ControlCenter?> Handle(GetControlCenterByIdQuery query, CancellationToken cancellationToken = default);
}
