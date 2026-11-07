namespace SecurityBus.API.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when deleting a vehicle.
/// </summary>
public enum DeleteVehicleError
{
	/// <summary>
	/// The vehicle was not found.
	/// </summary>
	VehicleNotFound,

	/// <summary>
	/// The vehicle has an active assignment and cannot be deleted.
	/// </summary>
	VehicleHasActiveAssignment,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
