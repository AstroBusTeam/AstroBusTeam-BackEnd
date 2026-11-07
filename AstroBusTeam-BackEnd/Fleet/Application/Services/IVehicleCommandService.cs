using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Command service interface for vehicle operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling vehicle commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IVehicleCommandService
{
    /// <summary>
    ///     Handle the create vehicle command.
    /// </summary>
    /// <param name="command">CreateVehicleCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the vehicle on success, or the error reason on failure.
    /// </returns>
    Task<Result<Vehicle, CreateVehicleError>> Handle(CreateVehicleCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the update vehicle command.
    /// </summary>
    /// <param name="command">UpdateVehicleCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the vehicle on success, or the error reason on failure.
    /// </returns>
    Task<Result<Vehicle, UpdateVehicleError>> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the delete vehicle command.
    /// </summary>
    /// <param name="command">DeleteVehicleCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the vehicle on success, or the error reason on failure.
    /// </returns>
    Task<Result<Vehicle, DeleteVehicleError>> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken = default);
}
