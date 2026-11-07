using SecurityBus.API.Shared.Domain.Model;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Interfaces.REST.Resources;

namespace SecurityBus.API.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembles a ShiftResource from a Shift.
/// </summary>
public static class ShiftResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a ShiftResource from a Shift.
    /// </summary>
    /// <param name="entity">The Shift entity</param>
    /// <returns>
    ///     A ShiftResource assembled from the Shift
    /// </returns>
    public static ShiftResource ToResourceFromEntity(Shift entity) =>
        new(entity.Id,
            entity.DriverId,
            entity.VehicleId,
            entity.RouteCode,
            entity.RouteName,
            entity.Date,
            entity.StartTime,
            entity.EndTime,
            entity.DistanceKm,
            entity.Passengers,
            entity.Revenue,
            entity.Incidents,
            entity.Status.Value,
            entity.SpeedKmh,
            entity.MaxSpeedKmh,
            entity.AverageSpeedKmh,
            entity.SpeedSamples,
            entity.OverspeedEvents);
}
