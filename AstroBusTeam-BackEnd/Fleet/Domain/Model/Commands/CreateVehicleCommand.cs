using SecurityBus.API.Fleet.Domain.Model.ValueObjects;

namespace SecurityBus.API.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to create a vehicle
/// </summary>
/// <param name="Code">Unit code</param>
/// <param name="Plate">License plate</param>
/// <param name="Model">Model</param>
/// <param name="Capacity">Passenger capacity</param>
/// <param name="Status">Vehicle status (available, in-service or maintenance)</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record CreateVehicleCommand(VehicleCode Code, LicensePlate Plate, string Model, int Capacity, VehicleStatus Status, double Latitude, double Longitude);
