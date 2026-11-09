using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to delete a vehicle
/// </summary>
/// <param name="Id">The identifier of the vehicle to delete</param>
public record DeleteVehicleCommand(int Id);
