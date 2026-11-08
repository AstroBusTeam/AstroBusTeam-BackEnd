namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a route
/// </summary>
public record BusRouteResource(
    int Id,
    string Code,
    string Name,
    string Origin,
    string Destination,
    decimal DistanceKm,
    bool Active);
