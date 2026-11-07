namespace SecurityBus.API.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when updating a vehicle.
/// </summary>
public enum UpdateVehicleError
{
	/// <summary>
	/// The vehicle was not found.
	/// </summary>
	VehicleNotFound,

	/// <summary>
	/// The vehicle data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// A vehicle with this unit code already exists.
	/// </summary>
	DuplicateVehicleCode,

	/// <summary>
	/// A vehicle with this license plate already exists.
	/// </summary>
	DuplicateLicensePlate,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
