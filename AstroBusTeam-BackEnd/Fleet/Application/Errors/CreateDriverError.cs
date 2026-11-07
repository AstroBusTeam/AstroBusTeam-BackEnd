namespace SecurityBus.API.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a driver.
/// </summary>
public enum CreateDriverError
{
	/// <summary>
	/// The driver data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// A driver with this employee code already exists.
	/// </summary>
	DuplicateEmployeeCode,

	/// <summary>
	/// A driver with this DNI already exists.
	/// </summary>
	DuplicateDni,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
