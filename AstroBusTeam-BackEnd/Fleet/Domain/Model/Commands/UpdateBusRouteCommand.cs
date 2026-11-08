namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to update a route
/// </summary>
public record UpdateBusRouteCommand(int Id, string Code, string Name, string Origin, string Destination, decimal DistanceKm, bool Active);
