using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for notification channels
/// </summary>
public class NotificationChannelQueryService(INotificationChannelRepository repository) : INotificationChannelQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<NotificationChannel>> Handle(GetAllNotificationChannelsQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<NotificationChannel?> Handle(GetNotificationChannelByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
