using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Application.Patterns;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Command service for notification channels
/// </summary>
public interface INotificationChannelCommandService
{
    /// <summary>
    ///     Handles the creation of a notification channel.
    /// </summary>
    Task<Result<NotificationChannel, CreateNotificationChannelError>> Handle(CreateNotificationChannelCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update of a notification channel.
    /// </summary>
    Task<Result<NotificationChannel, UpdateNotificationChannelError>> Handle(UpdateNotificationChannelCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the deletion of a notification channel.
    /// </summary>
    Task<Result<NotificationChannel, DeleteNotificationChannelError>> Handle(DeleteNotificationChannelCommand command, CancellationToken cancellationToken = default);
}
