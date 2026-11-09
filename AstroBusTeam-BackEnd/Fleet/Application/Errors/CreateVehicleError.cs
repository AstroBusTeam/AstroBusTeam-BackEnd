namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a vehicle.
/// </summary>
public enum CreateVehicleError
{
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
