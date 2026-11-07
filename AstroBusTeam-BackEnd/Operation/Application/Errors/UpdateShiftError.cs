namespace SecurityBus.API.Operations.Application.Errors;

/// <summary>
/// Represents errors that can occur when updating a shift.
/// </summary>
public enum UpdateShiftError
{
	/// <summary>
	/// The shift was not found.
	/// </summary>
	ShiftNotFound,

	/// <summary>
	/// The shift data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// A shift that has ended cannot be reopened.
	/// </summary>
	InvalidShiftStateTransition,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
