using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to update a vehicle
/// </summary>
public record UpdateVehicleCommand(int Id, VehicleCode Code, LicensePlate Plate, string Model, int Capacity, VehicleStatus Status, double CurrentLatitude, double CurrentLongitude);
