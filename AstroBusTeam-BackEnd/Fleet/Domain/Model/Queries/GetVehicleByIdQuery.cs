namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

/// <summary>
///     Query to get a vehicle by ID
/// </summary>
/// <param name="Id">The vehicle ID to search</param>
public record GetVehicleByIdQuery(int Id);
