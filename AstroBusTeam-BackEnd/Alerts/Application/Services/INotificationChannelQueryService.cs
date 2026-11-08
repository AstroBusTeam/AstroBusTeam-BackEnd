using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service for notification channels
/// </summary>
public interface INotificationChannelQueryService
{
    /// <summary>
    ///     Handles the listing of notification channels.
    /// </summary>
    Task<IEnumerable<NotificationChannel>> Handle(GetAllNotificationChannelsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the lookup of a notification channel by identifier.
    /// </summary>
    Task<NotificationChannel?> Handle(GetNotificationChannelByIdQuery query, CancellationToken cancellationToken = default);
}
