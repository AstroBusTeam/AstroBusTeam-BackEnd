using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to update a vehicle
/// </summary>
/// <param name="Id">The identifier of the vehicle</param>
/// <param name="Code">Unit code</param>
/// <param name="Plate">License plate</param>
/// <param name="Model">Model</param>
/// <param name="Capacity">Passenger capacity</param>
/// <param name="Status">Vehicle status (available, in-service or maintenance)</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record UpdateVehicleCommand(int Id, VehicleCode Code, LicensePlate Plate, string Model, int Capacity, VehicleStatus Status, double Latitude, double Longitude);
