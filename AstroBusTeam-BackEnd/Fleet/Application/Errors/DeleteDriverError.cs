namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when deleting a driver.
/// </summary>
public enum DeleteDriverError
{
	/// <summary>
	/// The driver was not found.
	/// </summary>
	DriverNotFound,

	/// <summary>
	/// The driver has an active assignment and cannot be deleted.
	/// </summary>
	DriverHasActiveAssignment,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
