using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a vehicle position
/// </summary>
public record CreateVehiclePositionResource(
    int VehicleId,
    double Latitude,
    double Longitude,
    int? ShiftId = null,
    double? SpeedKmh = null,
    DateTime? RecordedAt = null);
