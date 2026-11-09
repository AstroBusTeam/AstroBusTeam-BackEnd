namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteNotificationChannelError operation
/// </summary>
public enum DeleteNotificationChannelError
{
	NotificationChannelNotFound,

	NotificationChannelHasRelatedData,

	UnexpectedError
}
