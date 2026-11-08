using System.Globalization;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create assignment resource into a command
/// </summary>
public static class CreateAssignmentCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateAssignmentCommand ToCommandFromResource(CreateAssignmentResource resource)
    {
        return new CreateAssignmentCommand(
            resource.DriverId,
            resource.VehicleId,
            resource.RouteId,
            TimeOnly.ParseExact(resource.StartTime, ["HH:mm", "HH:mm:ss"], CultureInfo.InvariantCulture),
            resource.ValidFrom ?? DateOnly.FromDateTime(LocalClock.Now()),
            resource.ValidTo,
            new AssignmentStatus(resource.Status),
            resource.AssignedBy);
    }
}
