namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateAlertTypeError operation
/// </summary>
public enum UpdateAlertTypeError
{
	AlertTypeNotFound,

	InvalidData,

	DuplicateName,

	UnexpectedError
}
