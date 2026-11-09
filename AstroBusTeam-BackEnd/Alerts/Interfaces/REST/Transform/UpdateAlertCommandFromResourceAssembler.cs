using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update alert resource into a command
/// </summary>
public static class UpdateAlertCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateAlertCommand ToCommandFromResource(int id, UpdateAlertResource resource) =>
        new(id,
            new AlertCode(resource.Code),
            resource.VehicleId,
            resource.DriverId,
            resource.ShiftId,
            resource.AlertTypeId,
            resource.ReceiverId,
            resource.Attempts,
            new AlertStatus(resource.Status),
            resource.CreatedAt,
            resource.Latitude,
            resource.Longitude,
            resource.ConfirmedAt,
            resource.ConfirmedBy,
            resource.Notes);
}
