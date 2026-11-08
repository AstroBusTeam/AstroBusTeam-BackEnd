using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create shift resource into a command
/// </summary>
public static class CreateShiftCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateShiftCommand ToCommandFromResource(CreateShiftResource resource)
    {
        var startTime = resource.StartTime ?? LocalClock.Now();
        return new CreateShiftCommand(
            resource.AssignmentId,
            resource.DriverId,
            resource.VehicleId,
            resource.RouteId,
            resource.ShiftDate ?? DateOnly.FromDateTime(startTime),
            startTime,
            resource.EndTime,
            resource.DistanceKm ?? 0,
            resource.Passengers ?? 0,
            resource.Revenue ?? 0,
            new ShiftStatus(resource.Status ?? "active"),
            resource.AverageSpeedKmh ?? 0,
            resource.MaxSpeedKmh ?? 0,
            resource.OverspeedEvents ?? 0);
    }
}
