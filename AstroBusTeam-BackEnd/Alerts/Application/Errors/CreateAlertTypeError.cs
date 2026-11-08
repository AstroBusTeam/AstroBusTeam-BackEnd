namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the CreateAlertTypeError operation
/// </summary>
public enum CreateAlertTypeError
{
	InvalidData,

	DuplicateName,

	UnexpectedError
}
