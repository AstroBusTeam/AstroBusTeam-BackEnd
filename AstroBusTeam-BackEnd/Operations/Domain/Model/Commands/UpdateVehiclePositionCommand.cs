namespace AstroBusTeamBackEnd.Operations.Domain.Model.Commands;

/// <summary>
///     Command to update a vehicle position
/// </summary>
public record UpdateVehiclePositionCommand(int Id, int VehicleId, int? ShiftId, double Latitude, double Longitude, double SpeedKmh, DateTime RecordedAt);
