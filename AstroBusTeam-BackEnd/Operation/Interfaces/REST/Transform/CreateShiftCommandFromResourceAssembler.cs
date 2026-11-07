using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Operations.Domain.Model.Commands;
using SecurityBus.API.Operations.Domain.Model.ValueObjects;
using SecurityBus.API.Operations.Interfaces.REST.Resources;

namespace SecurityBus.API.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembles a CreateShiftCommand from a CreateShiftResource.
/// </summary>
public static class CreateShiftCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a CreateShiftCommand from a CreateShiftResource.
    /// </summary>
    /// <param name="resource">The CreateShiftResource resource</param>
    /// <returns>
    ///     A CreateShiftCommand assembled from the CreateShiftResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static CreateShiftCommand ToCommandFromResource(CreateShiftResource resource)
    {
        var startTime = resource.StartTime ?? LocalClock.Now();
        return new CreateShiftCommand(
            resource.DriverId,
            resource.VehicleId,
            resource.RouteCode,
            resource.RouteName,
            resource.Date ?? DateOnly.FromDateTime(startTime),
            startTime,
            resource.EndTime,
            resource.DistanceKm ?? 0,
            resource.Passengers ?? 0,
            resource.Revenue ?? 0,
            resource.Incidents ?? 0,
            new ShiftStatus(resource.Status ?? "active"),
            resource.SpeedKmh ?? 0,
            resource.MaxSpeedKmh ?? 0,
            resource.AverageSpeedKmh ?? 0,
            resource.SpeedSamples ?? 0,
            resource.OverspeedEvents ?? 0);
    }
}
