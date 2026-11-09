using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Command service for alert types
/// </summary>
public interface IAlertTypeCommandService
{
    /// <summary>
    ///     Handles the creation of a alert type.
    /// </summary>
    Task<Result<AlertType, CreateAlertTypeError>> Handle(CreateAlertTypeCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a alert type.
    /// </summary>
    Task<Result<AlertType, UpdateAlertTypeError>> Handle(UpdateAlertTypeCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a alert type.
    /// </summary>
    Task<Result<AlertType, DeleteAlertTypeError>> Handle(DeleteAlertTypeCommand command, CancellationToken cancellationToken = default);
}
