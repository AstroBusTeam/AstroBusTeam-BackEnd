namespace SecurityBus.API.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a assignment.
/// </summary>
public enum CreateAssignmentError
{
	/// <summary>
	/// The assignment data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// The driver of the assignment does not exist.
	/// </summary>
	DriverNotFound,

	/// <summary>
	/// The vehicle of the assignment does not exist.
	/// </summary>
	VehicleNotFound,

	/// <summary>
	/// The driver is not active and cannot be assigned.
	/// </summary>
	DriverNotActive,

	/// <summary>
	/// The vehicle is in maintenance and cannot be assigned.
	/// </summary>
	VehicleInMaintenance,

	/// <summary>
	/// The driver already has an active assignment.
	/// </summary>
	DriverAlreadyAssigned,

	/// <summary>
	/// The vehicle already has an active assignment.
	/// </summary>
	VehicleAlreadyAssigned,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
