namespace AstroBusTeamBackEnd.Operations.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateVehiclePositionError operation
/// </summary>
public enum UpdateVehiclePositionError
{
	VehiclePositionNotFound,

	InvalidData,

	InvalidReference,

	UnexpectedError
}
