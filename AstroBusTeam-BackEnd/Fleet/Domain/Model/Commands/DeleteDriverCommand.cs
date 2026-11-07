using SecurityBus.API.Fleet.Domain.Model.ValueObjects;

namespace SecurityBus.API.Fleet.Domain.Model.Commands;

/// <summary>
///     Command to delete a driver
/// </summary>
/// <param name="Id">The identifier of the driver to delete</param>
public record DeleteDriverCommand(int Id);
