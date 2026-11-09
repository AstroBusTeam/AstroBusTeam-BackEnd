namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateBusRouteError operation
/// </summary>
public enum UpdateBusRouteError
{
	BusRouteNotFound,

	InvalidData,

	DuplicateCode,

	UnexpectedError
}
