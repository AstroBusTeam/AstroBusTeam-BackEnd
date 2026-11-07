using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

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
