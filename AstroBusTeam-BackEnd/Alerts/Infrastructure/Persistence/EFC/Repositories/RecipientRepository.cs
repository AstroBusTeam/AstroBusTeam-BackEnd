using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for recipients
/// </summary>
public class RecipientRepository(AppDbContext context)
    : BaseRepository<Recipient>(context), IRecipientRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Recipient>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Recipient>()
            .Include(r => r.Channels)
            .OrderBy(r => r.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Recipient?> FindByIdWithChannelsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Recipient>()
            .Include(r => r.Channels)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
