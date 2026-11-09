namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateControlCenterError operation
/// </summary>
public enum UpdateControlCenterError
{
	ControlCenterNotFound,

	InvalidData,

	DuplicateCode,

	UnexpectedError
}
