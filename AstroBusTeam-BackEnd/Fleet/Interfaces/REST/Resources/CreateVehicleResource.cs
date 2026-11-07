using System.ComponentModel.DataAnnotations;

namespace SecurityBus.API.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to create a vehicle.
/// </summary>
/// <param name="Code">Unit code</param>
/// <param name="Plate">License plate</param>
/// <param name="Model">Model</param>
/// <param name="Capacity">Passenger capacity</param>
/// <param name="Status">Vehicle status (available, in-service or maintenance)</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record CreateVehicleResource(
    [Required(ErrorMessage = "RequiredVehicleCode")]
    string Code,
    [Required(ErrorMessage = "RequiredVehiclePlate")]
    string Plate,
    [Required(ErrorMessage = "RequiredVehicleModel")]
    string Model,
    int Capacity,
    [Required(ErrorMessage = "RequiredVehicleStatus")]
    string Status,
    double Latitude,
    double Longitude);
