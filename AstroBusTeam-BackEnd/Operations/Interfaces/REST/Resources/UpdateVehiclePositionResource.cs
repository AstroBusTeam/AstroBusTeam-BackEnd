using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a vehicle position
/// </summary>
public record UpdateVehiclePositionResource(
    int VehicleId,
    double Latitude,
    double Longitude,
    double SpeedKmh,
    DateTime RecordedAt,
    int? ShiftId = null);
