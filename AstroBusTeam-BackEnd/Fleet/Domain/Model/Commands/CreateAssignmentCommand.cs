using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to create an assignment
/// </summary>
public record CreateAssignmentCommand(int DriverId, int VehicleId, int RouteId, TimeOnly StartTime, DateOnly ValidFrom, DateOnly? ValidTo, AssignmentStatus Status, int? AssignedBy);
