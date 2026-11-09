using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     Repository for notification channels
/// </summary>
public interface INotificationChannelRepository : IBaseRepository<NotificationChannel>
{
    /// <summary>
    ///     Finds notification channels, optionally filtered.
    /// </summary>
    Task<IEnumerable<NotificationChannel>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<NotificationChannel?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
}
