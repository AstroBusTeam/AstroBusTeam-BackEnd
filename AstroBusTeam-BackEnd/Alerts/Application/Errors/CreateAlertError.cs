namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a alert.
/// </summary>
public enum CreateAlertError
{
	/// <summary>
	/// The alert data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// The vehicle of the alert does not exist.
	/// </summary>
	VehicleNotFound,

	/// <summary>
	/// An alert with this code already exists.
	/// </summary>
	DuplicateAlertCode,

	/// <summary>
	/// A referenced record (driver, shift, alert type, receiver or user) does not exist.
	/// </summary>
	InvalidReference,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
