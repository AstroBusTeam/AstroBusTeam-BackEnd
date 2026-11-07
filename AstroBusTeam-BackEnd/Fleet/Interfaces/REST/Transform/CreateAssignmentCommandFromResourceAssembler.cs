using System.Globalization;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateAssignmentCommand from a CreateAssignmentResource.
/// </summary>
public static class CreateAssignmentCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateAssignmentCommand from a CreateAssignmentResource.
    /// </summary>
    /// <param name="resource">The CreateAssignmentResource resource</param>
    /// <returns>
    ///     A CreateAssignmentCommand assembled from the CreateAssignmentResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateAssignmentCommand ToCommandFromResource(CreateAssignmentResource resource)
    {
        return new CreateAssignmentCommand(
            resource.DriverId,
            resource.VehicleId,
            resource.Route,
            TimeOnly.ParseExact(resource.StartTime, ["HH:mm", "HH:mm:ss"], CultureInfo.InvariantCulture),
            new AssignmentStatus(resource.Status));
    }
}
