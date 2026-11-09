using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Command service interface for driver operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling driver commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IDriverCommandService
{
    /// <summary>
    ///     Handle the create driver command.
    /// </summary>
    /// <param name="command">CreateDriverCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the driver on success, or the error reason on failure.
    /// </returns>
    Task<Result<Driver, CreateDriverError>> Handle(CreateDriverCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the update driver command.
    /// </summary>
    /// <param name="command">UpdateDriverCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the driver on success, or the error reason on failure.
    /// </returns>
    Task<Result<Driver, UpdateDriverError>> Handle(UpdateDriverCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the delete driver command.
    /// </summary>
    /// <param name="command">DeleteDriverCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the driver on success, or the error reason on failure.
    /// </returns>
    Task<Result<Driver, DeleteDriverError>> Handle(DeleteDriverCommand command, CancellationToken cancellationToken = default);
}
