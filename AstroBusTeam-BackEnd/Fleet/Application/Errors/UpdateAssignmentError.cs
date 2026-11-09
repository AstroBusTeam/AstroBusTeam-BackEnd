namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
///     Errors that can occur when updating an assignment
/// </summary>
public enum UpdateAssignmentError
{
	/// <summary>
	/// The assignment does not exist.
	/// </summary>
	AssignmentNotFound,

	/// <summary>
	/// The data is invalid.
	/// </summary>
	InvalidData,

	/// <summary>
	/// The driver already has another active assignment.
	/// </summary>
	DriverAlreadyAssigned,

	/// <summary>
	/// The vehicle already has another active assignment.
	/// </summary>
	VehicleAlreadyAssigned,

	/// <summary>
	/// An unexpected error occurred.
	/// </summary>
	UnexpectedError
}
