using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Alerts.Application.Services;

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
