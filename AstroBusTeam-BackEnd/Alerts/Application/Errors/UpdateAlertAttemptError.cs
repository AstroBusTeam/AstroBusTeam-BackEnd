namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateAlertAttemptError operation
/// </summary>
public enum UpdateAlertAttemptError
{
	AlertAttemptNotFound,

	InvalidData,

	InvalidReference,

	UnexpectedError
}
