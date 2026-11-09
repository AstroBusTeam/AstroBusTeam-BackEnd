using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a route
/// </summary>
public record UpdateBusRouteResource(
    [Required(ErrorMessage = "RequiredBusRouteCode")]
    string Code,
    [Required(ErrorMessage = "RequiredBusRouteName")]
    string Name,
    [Required(ErrorMessage = "RequiredBusRouteOrigin")]
    string Origin,
    [Required(ErrorMessage = "RequiredBusRouteDestination")]
    string Destination,
    decimal DistanceKm,
    bool Active);
