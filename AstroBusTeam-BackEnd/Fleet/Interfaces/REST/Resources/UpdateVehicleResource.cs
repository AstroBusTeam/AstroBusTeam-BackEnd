using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a vehicle
/// </summary>
public record UpdateVehicleResource(
    [Required(ErrorMessage = "RequiredVehicleCode")]
    string Code,
    [Required(ErrorMessage = "RequiredVehiclePlate")]
    string Plate,
    [Required(ErrorMessage = "RequiredVehicleModel")]
    string Model,
    int Capacity,
    [Required(ErrorMessage = "RequiredVehicleStatus")]
    string Status,
    double CurrentLatitude,
    double CurrentLongitude);
