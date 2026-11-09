namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
///     Errors that can occur in the CreateBusRouteError operation
/// </summary>
public enum CreateBusRouteError
{
	InvalidData,

	DuplicateCode,

	UnexpectedError
}
