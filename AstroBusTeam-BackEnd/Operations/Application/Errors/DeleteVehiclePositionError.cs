namespace AstroBusTeamBackEnd.Operations.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteVehiclePositionError operation
/// </summary>
public enum DeleteVehiclePositionError
{
	VehiclePositionNotFound,

	VehiclePositionHasRelatedData,

	UnexpectedError
}
