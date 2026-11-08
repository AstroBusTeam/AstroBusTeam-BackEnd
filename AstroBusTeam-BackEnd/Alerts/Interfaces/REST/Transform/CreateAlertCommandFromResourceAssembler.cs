using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create alert resource into a command
/// </summary>
public static class CreateAlertCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
    {
        return new CreateAlertCommand(
            new AlertCode(resource.Code ?? AlertCode.Generate().Value),
            resource.VehicleId,
            resource.DriverId,
            resource.ShiftId,
            resource.AlertTypeId,
            resource.ReceiverId,
            resource.Attempts ?? 1,
            new AlertStatus(resource.Status ?? "pending"),
            resource.CreatedAt ?? LocalClock.Now(),
            resource.Latitude,
            resource.Longitude,
            resource.ConfirmedAt,
            resource.ConfirmedBy,
            resource.Notes);
    }
}
