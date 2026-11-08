namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteControlCenterError operation
/// </summary>
public enum DeleteControlCenterError
{
	ControlCenterNotFound,

	ControlCenterHasRelatedData,

	UnexpectedError
}
