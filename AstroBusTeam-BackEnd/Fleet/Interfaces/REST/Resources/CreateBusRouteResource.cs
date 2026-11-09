using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a route
/// </summary>
public record CreateBusRouteResource(
    [Required(ErrorMessage = "RequiredBusRouteCode")]
    string Code,
    [Required(ErrorMessage = "RequiredBusRouteName")]
    string Name,
    [Required(ErrorMessage = "RequiredBusRouteOrigin")]
    string Origin,
    [Required(ErrorMessage = "RequiredBusRouteDestination")]
    string Destination,
    decimal? DistanceKm = null,
    bool? Active = null);
