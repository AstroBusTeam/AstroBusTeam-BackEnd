namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteBusRouteError operation
/// </summary>
public enum DeleteBusRouteError
{
	BusRouteNotFound,

	BusRouteHasRelatedData,

	UnexpectedError
}
