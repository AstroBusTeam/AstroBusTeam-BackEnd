namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateNotificationChannelError operation
/// </summary>
public enum UpdateNotificationChannelError
{
	NotificationChannelNotFound,

	InvalidData,

	DuplicateCode,

	UnexpectedError
}
