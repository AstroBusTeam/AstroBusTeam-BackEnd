namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteAlertAttemptError operation
/// </summary>
public enum DeleteAlertAttemptError
{
	AlertAttemptNotFound,

	AlertAttemptHasRelatedData,

	UnexpectedError
}
