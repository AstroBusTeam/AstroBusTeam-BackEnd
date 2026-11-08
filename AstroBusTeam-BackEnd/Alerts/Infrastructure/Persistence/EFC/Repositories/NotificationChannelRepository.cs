using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for notification channels
/// </summary>
public class NotificationChannelRepository(AppDbContext context)
    : BaseRepository<NotificationChannel>(context), INotificationChannelRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<NotificationChannel>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<NotificationChannel>().AsQueryable();
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<NotificationChannel?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<NotificationChannel>().FirstOrDefaultAsync(e => e.Code == code, cancellationToken);
    }
}
