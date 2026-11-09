using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to delete a driver
/// </summary>
/// <param name="Id">The identifier of the driver to delete</param>
public record DeleteDriverCommand(int Id);
