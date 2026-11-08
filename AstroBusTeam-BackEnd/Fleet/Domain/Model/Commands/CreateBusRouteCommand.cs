namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to create a route
/// </summary>
public record CreateBusRouteCommand(string Code, string Name, string Origin, string Destination, decimal DistanceKm, bool Active);
