using SecurityBus.API.Operations.Domain.Model.ValueObjects;

namespace SecurityBus.API.Operations.Domain.Model.Commands;

/// <summary>
///     Command to create a shift
/// </summary>
/// <param name="DriverId">Driver ID</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="RouteCode">Route code</param>
/// <param name="RouteName">Route name</param>
/// <param name="Date">Shift date</param>
/// <param name="StartTime">Start date and time</param>
/// <param name="EndTime">End date and time (null while the shift is active)</param>
/// <param name="DistanceKm">Distance traveled in km</param>
/// <param name="Passengers">Passengers transported</param>
/// <param name="Revenue">Revenue collected</param>
/// <param name="Incidents">Incidents registered</param>
/// <param name="Status">Shift status (active, finished or interrupted)</param>
/// <param name="SpeedKmh">Current speed in km/h (0 when the shift ends)</param>
/// <param name="MaxSpeedKmh">Maximum speed in km/h</param>
/// <param name="AverageSpeedKmh">Average speed in km/h</param>
/// <param name="SpeedSamples">Number of speed samples taken</param>
/// <param name="OverspeedEvents">Times the speed limit was exceeded</param>
public record CreateShiftCommand(int DriverId, int VehicleId, string RouteCode, string RouteName, DateOnly Date, DateTime StartTime, DateTime? EndTime, double DistanceKm, int Passengers, decimal Revenue, int Incidents, ShiftStatus Status, double SpeedKmh, double MaxSpeedKmh, double AverageSpeedKmh, int SpeedSamples, int OverspeedEvents);
