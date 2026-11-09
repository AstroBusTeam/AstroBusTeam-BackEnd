using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a route of the SecurityBus platform
/// </summary>
public partial class BusRoute
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected BusRoute()
    {
        Code = string.Empty;
        Name = string.Empty;
        Origin = string.Empty;
        Destination = string.Empty;
    }

    /// <summary>
    ///     Creates a route from a command.
    /// </summary>
    /// <param name="command">The command with the route data.</param>
    public BusRoute(CreateBusRouteCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Name = command.Name;
        Origin = command.Origin;
        Destination = command.Destination;
        DistanceKm = command.DistanceKm;
        Active = command.Active;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public string Origin { get; private set; }

    public string Destination { get; private set; }

    public decimal DistanceKm { get; private set; }

    public bool Active { get; private set; }

    /// <summary>
    ///     Updates the route from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateBusRouteCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Name = command.Name;
        Origin = command.Origin;
        Destination = command.Destination;
        DistanceKm = command.DistanceKm;
        Active = command.Active;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Code, nameof(Code));
        Guard.NotLongerThan(Code, 20, nameof(Code));
        Guard.NotBlank(Name, nameof(Name));
        Guard.NotLongerThan(Name, 120, nameof(Name));
        Guard.NotBlank(Origin, nameof(Origin));
        Guard.NotLongerThan(Origin, 120, nameof(Origin));
        Guard.NotBlank(Destination, nameof(Destination));
        Guard.NotLongerThan(Destination, 120, nameof(Destination));
        Guard.AtLeast(DistanceKm, 0m, nameof(DistanceKm));
    }
}
