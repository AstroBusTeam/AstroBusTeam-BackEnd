using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Alerts.Application.Services;

/// <summary>
///     Command service interface for recipient operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling recipient commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IRecipientCommandService
{
    /// <summary>
    ///     Handle the update recipient command.
    /// </summary>
    /// <param name="command">UpdateRecipientCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the recipient on success, or the error reason on failure.
    /// </returns>
    Task<Result<Recipient, UpdateRecipientError>> Handle(UpdateRecipientCommand command, CancellationToken cancellationToken = default);
}
