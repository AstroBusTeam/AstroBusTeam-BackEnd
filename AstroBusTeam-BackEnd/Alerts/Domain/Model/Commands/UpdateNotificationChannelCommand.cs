namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a notification channel
/// </summary>
public record UpdateNotificationChannelCommand(int Id, string Code);
