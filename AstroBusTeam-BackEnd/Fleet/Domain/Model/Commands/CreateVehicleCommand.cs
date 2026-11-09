using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to create a vehicle
/// </summary>
public record CreateVehicleCommand(VehicleCode Code, LicensePlate Plate, string Model, int Capacity, VehicleStatus Status, double CurrentLatitude, double CurrentLongitude);
