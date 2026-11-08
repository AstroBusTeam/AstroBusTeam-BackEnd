namespace AstroBusTeamBackEnd.Operations.Domain.Model.Commands;

/// <summary>
///     Command to create a vehicle position
/// </summary>
public record CreateVehiclePositionCommand(int VehicleId, int? ShiftId, double Latitude, double Longitude, double SpeedKmh, DateTime RecordedAt);
