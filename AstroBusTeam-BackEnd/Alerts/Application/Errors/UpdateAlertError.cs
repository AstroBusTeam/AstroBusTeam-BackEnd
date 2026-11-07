namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
/// Represents errors that can occur when updating a alert.
/// </summary>
public enum UpdateAlertError
{
	/// <summary>
	/// The alert was not found.
	/// </summary>
	AlertNotFound,

	/// <summary>
	/// The alert data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// An alert with this code already exists.
	/// </summary>
	DuplicateAlertCode,

	/// <summary>
	/// The number of attempts of an alert cannot decrease.
	/// </summary>
	AttemptsCannotDecrease,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
