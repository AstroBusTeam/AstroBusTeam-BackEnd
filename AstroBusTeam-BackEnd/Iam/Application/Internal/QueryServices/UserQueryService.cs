using AstroBusTeamBackEnd.Iam.Application.Services;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Queries;
using AstroBusTeamBackEnd.Iam.Domain.Repositories;

namespace AstroBusTeamBackEnd.Iam.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for users
/// </summary>
public class UserQueryService(IUserRepository repository) : IUserQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
