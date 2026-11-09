using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Iam.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for users
/// </summary>
public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<User>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<User>().AsQueryable();
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(e => e.Username == username, cancellationToken);
    }
}
