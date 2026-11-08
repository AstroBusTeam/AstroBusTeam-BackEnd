using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Command service for control centers
/// </summary>
public interface IControlCenterCommandService
{
    /// <summary>
    ///     Handles the creation of a control center.
    /// </summary>
    Task<Result<ControlCenter, CreateControlCenterError>> Handle(CreateControlCenterCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a control center.
    /// </summary>
    Task<Result<ControlCenter, UpdateControlCenterError>> Handle(UpdateControlCenterCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a control center.
    /// </summary>
    Task<Result<ControlCenter, DeleteControlCenterError>> Handle(DeleteControlCenterCommand command, CancellationToken cancellationToken = default);
}
