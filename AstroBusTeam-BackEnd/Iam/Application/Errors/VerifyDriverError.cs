namespace SecurityBus.API.Iam.Application.Errors;

/// <summary>
/// Represents errors that can occur when verifying the identity of a driver.
/// </summary>
public enum VerifyDriverError
{
	/// <summary>
	/// No driver exists with the provided employee code.
	/// </summary>
	DriverNotFound,

	/// <summary>
	/// The driver exists but is not active.
	/// </summary>
	DriverNotActive,

	/// <summary>
	/// The driver does not have an active assignment to a unit.
	/// </summary>
	NoActiveAssignment,

	/// <summary>
	/// The assigned vehicle no longer exists.
	/// </summary>
	AssignedVehicleNotFound,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
