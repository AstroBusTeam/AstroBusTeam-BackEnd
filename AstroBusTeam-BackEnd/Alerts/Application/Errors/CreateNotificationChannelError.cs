namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the CreateNotificationChannelError operation
/// </summary>
public enum CreateNotificationChannelError
{
	InvalidData,

	DuplicateCode,

	UnexpectedError
}
