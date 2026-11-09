using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Operations.Domain.Model.Commands;

/// <summary>
///     Command to create a shift
/// </summary>
public record CreateShiftCommand(int AssignmentId, int DriverId, int VehicleId, int RouteId, DateOnly ShiftDate, DateTime StartTime, DateTime? EndTime, double DistanceKm, int Passengers, decimal Revenue, ShiftStatus Status, double AverageSpeedKmh, double MaxSpeedKmh, int OverspeedEvents);
