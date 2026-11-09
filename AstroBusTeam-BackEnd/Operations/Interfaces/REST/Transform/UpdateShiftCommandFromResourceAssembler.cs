using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update shift resource into a command
/// </summary>
public static class UpdateShiftCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateShiftCommand ToCommandFromResource(int id, UpdateShiftResource resource) =>
        new(id,
            resource.AssignmentId,
            resource.DriverId,
            resource.VehicleId,
            resource.RouteId,
            resource.ShiftDate,
            resource.StartTime,
            resource.EndTime,
            resource.DistanceKm,
            resource.Passengers,
            resource.Revenue,
            new ShiftStatus(resource.Status),
            resource.AverageSpeedKmh,
            resource.MaxSpeedKmh,
            resource.OverspeedEvents);
}
