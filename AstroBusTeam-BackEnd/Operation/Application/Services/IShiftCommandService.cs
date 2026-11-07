using SecurityBus.API.Operations.Application.Errors;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.Commands;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Operations.Application.Services;

/// <summary>
///     Command service interface for shift operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling shift commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IShiftCommandService
{
    /// <summary>
    ///     Handle the create shift command.
    /// </summary>
    /// <param name="command">CreateShiftCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the shift on success, or the error reason on failure.
    /// </returns>
    Task<Result<Shift, CreateShiftError>> Handle(CreateShiftCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the update shift command.
    /// </summary>
    /// <param name="command">UpdateShiftCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the shift on success, or the error reason on failure.
    /// </returns>
    Task<Result<Shift, UpdateShiftError>> Handle(UpdateShiftCommand command, CancellationToken cancellationToken = default);
}
