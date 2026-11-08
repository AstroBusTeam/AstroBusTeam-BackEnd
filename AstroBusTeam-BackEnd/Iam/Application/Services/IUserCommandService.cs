using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Iam.Application.Services;

/// <summary>
///     Command service for users
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    ///     Handles the creation of a user.
    /// </summary>
    Task<Result<User, CreateUserError>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a user.
    /// </summary>
    Task<Result<User, UpdateUserError>> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a user.
    /// </summary>
    Task<Result<User, DeleteUserError>> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default);
}
