using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Command service interface for assignment operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for handling assignment commands. Expected failures are reported
///     through <see cref="Result{TValue, TError}"/> instead of exceptions.
/// </remarks>
public interface IAssignmentCommandService
{
    /// <summary>
    ///     Handle the create assignment command.
    /// </summary>
    /// <param name="command">CreateAssignmentCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the assignment on success, or the error reason on failure.
    /// </returns>
    Task<Result<Assignment, CreateAssignmentError>> Handle(CreateAssignmentCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the delete assignment command.
    /// </summary>
    /// <param name="command">DeleteAssignmentCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the assignment on success, or the error reason on failure.
    /// </returns>
    Task<Result<Assignment, DeleteAssignmentError>> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken = default);
}
