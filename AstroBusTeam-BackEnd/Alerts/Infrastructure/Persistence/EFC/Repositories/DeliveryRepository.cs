using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for delivery persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with delivery-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class DeliveryRepository(AppDbContext context)
    : BaseRepository<Delivery>(context), IDeliveryRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Delivery>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Delivery>().AsQueryable();

        return await query.OrderBy(d => d.Id).ToListAsync(cancellationToken);
    }
}
