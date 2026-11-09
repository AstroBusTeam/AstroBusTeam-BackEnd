using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Iam.Domain.Repositories;

/// <summary>
///     Repository for users
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Finds users, optionally filtered.
    /// </summary>
    Task<IEnumerable<User>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
