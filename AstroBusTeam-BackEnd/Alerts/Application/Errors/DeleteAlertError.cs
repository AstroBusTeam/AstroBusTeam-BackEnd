namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
/// Represents errors that can occur when deleting a alert.
/// </summary>
public enum DeleteAlertError
{
	/// <summary>
	/// The alert was not found.
	/// </summary>
	AlertNotFound,

	/// <summary>
	/// The alert is referenced by other records (deliveries) and cannot be deleted.
	/// </summary>
	AlertHasRelatedData,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
