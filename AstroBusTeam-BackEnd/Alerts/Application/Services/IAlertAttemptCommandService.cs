using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Command service for alert attempts
/// </summary>
public interface IAlertAttemptCommandService
{
    /// <summary>
    ///     Handles the creation of a alert attempt.
    /// </summary>
    Task<Result<AlertAttempt, CreateAlertAttemptError>> Handle(CreateAlertAttemptCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a alert attempt.
    /// </summary>
    Task<Result<AlertAttempt, UpdateAlertAttemptError>> Handle(UpdateAlertAttemptCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a alert attempt.
    /// </summary>
    Task<Result<AlertAttempt, DeleteAlertAttemptError>> Handle(DeleteAlertAttemptCommand command, CancellationToken cancellationToken = default);
}
