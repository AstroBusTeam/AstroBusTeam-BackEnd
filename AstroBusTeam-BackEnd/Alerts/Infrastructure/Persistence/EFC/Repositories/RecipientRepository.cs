using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for recipient persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with recipient-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class RecipientRepository(AppDbContext context)
    : BaseRepository<Recipient>(context), IRecipientRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Recipient>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Recipient>().AsQueryable();

        return await query.OrderBy(r => r.Id).ToListAsync(cancellationToken);
    }
}
