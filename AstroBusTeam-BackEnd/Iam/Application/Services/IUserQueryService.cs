using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Iam.Application.Services;

/// <summary>
///     Query service for users
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    ///     Handles the listing of users.
    /// </summary>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a user by identifier.
    /// </summary>
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default);
}
