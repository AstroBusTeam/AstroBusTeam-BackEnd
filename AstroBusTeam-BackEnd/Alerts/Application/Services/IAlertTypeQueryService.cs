using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service for alert types
/// </summary>
public interface IAlertTypeQueryService
{
    /// <summary>
    ///     Handles the listing of alert types.
    /// </summary>
    Task<IEnumerable<AlertType>> Handle(GetAllAlertTypesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a alert type by identifier.
    /// </summary>
    Task<AlertType?> Handle(GetAlertTypeByIdQuery query, CancellationToken cancellationToken = default);
}
