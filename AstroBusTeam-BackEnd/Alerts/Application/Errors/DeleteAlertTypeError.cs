namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteAlertTypeError operation
/// </summary>
public enum DeleteAlertTypeError
{
	AlertTypeNotFound,

	AlertTypeHasRelatedData,

	UnexpectedError
}
