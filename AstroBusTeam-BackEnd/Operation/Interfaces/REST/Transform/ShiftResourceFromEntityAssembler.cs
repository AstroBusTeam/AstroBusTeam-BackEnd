using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

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
