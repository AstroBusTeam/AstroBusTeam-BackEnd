using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a shift
/// </summary>
public record UpdateShiftResource(
    int AssignmentId,
    int DriverId,
    int VehicleId,
    int RouteId,
    DateOnly ShiftDate,
    DateTime StartTime,
    double DistanceKm,
    int Passengers,
    decimal Revenue,
    [Required(ErrorMessage = "RequiredShiftStatus")]
    string Status,
    double AverageSpeedKmh,
    double MaxSpeedKmh,
    int OverspeedEvents,
    DateTime? EndTime = null);
