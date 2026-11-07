namespace AstroBusTeamBackEnd.Operations.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a shift.
/// </summary>
public enum CreateShiftError
{
	/// <summary>
	/// The shift data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// The driver of the shift does not exist.
	/// </summary>
	DriverNotFound,

	/// <summary>
	/// The vehicle of the shift does not exist.
	/// </summary>
	VehicleNotFound,

	/// <summary>
	/// The driver already has an active shift.
	/// </summary>
	ActiveShiftAlreadyExists,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
