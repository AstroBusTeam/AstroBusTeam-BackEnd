using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Alerts.Application.Services;

/// <summary>
///     Command service interface for alert operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling alert commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IAlertCommandService
{
    /// <summary>
    ///     Handle the create alert command.
    /// </summary>
    /// <param name="command">CreateAlertCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the alert on success, or the error reason on failure.
    /// </returns>
    Task<Result<Alert, CreateAlertError>> Handle(CreateAlertCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the update alert command.
    /// </summary>
    /// <param name="command">UpdateAlertCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the alert on success, or the error reason on failure.
    /// </returns>
    Task<Result<Alert, UpdateAlertError>> Handle(UpdateAlertCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the delete alert command.
    /// </summary>
    /// <param name="command">DeleteAlertCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the alert on success, or the error reason on failure.
    /// </returns>
    Task<Result<Alert, DeleteAlertError>> Handle(DeleteAlertCommand command, CancellationToken cancellationToken = default);
}
