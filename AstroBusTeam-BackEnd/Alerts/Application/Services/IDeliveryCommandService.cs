using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Command service interface for delivery operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling delivery commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IDeliveryCommandService
{
    /// <summary>
    ///     Handle the create delivery command.
    /// </summary>
    /// <param name="command">CreateDeliveryCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the delivery on success, or the error reason on failure.
    /// </returns>
    Task<Result<Delivery, CreateDeliveryError>> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken = default);
}
