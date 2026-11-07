using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Operations.Domain.Model.Commands;
using SecurityBus.API.Operations.Domain.Model.ValueObjects;
using SecurityBus.API.Operations.Interfaces.REST.Resources;

namespace SecurityBus.API.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembles an UpdateShiftCommand from an UpdateShiftResource.
/// </summary>
public static class UpdateShiftCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles an UpdateShiftCommand from an UpdateShiftResource.
    /// </summary>
    /// <param name="id">The identifier of the shift taken from the route</param>
    /// <param name="resource">The UpdateShiftResource resource</param>
    /// <returns>
    ///     An UpdateShiftCommand assembled from the UpdateShiftResource
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when a value object rejects its raw value.</exception>
    public static UpdateShiftCommand ToCommandFromResource(int id, UpdateShiftResource resource) =>
        new(id,
            resource.DriverId,
            resource.VehicleId,
            resource.RouteCode,
            resource.RouteName,
            resource.Date,
            resource.StartTime,
            resource.EndTime,
            resource.DistanceKm,
            resource.Passengers,
            resource.Revenue,
            resource.Incidents,
            new ShiftStatus(resource.Status),
            resource.SpeedKmh,
            resource.MaxSpeedKmh,
            resource.AverageSpeedKmh,
            resource.SpeedSamples,
            resource.OverspeedEvents);
}
