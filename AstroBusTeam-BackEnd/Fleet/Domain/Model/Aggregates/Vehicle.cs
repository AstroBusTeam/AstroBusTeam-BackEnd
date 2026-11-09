using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a vehicle (bus) of the fleet
/// </summary>
/// <remarks>
///     The vehicle keeps its last known position. <see cref="LastPositionAt"/> is refreshed whenever the position
///     changes.
/// </remarks>
public partial class Vehicle
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Vehicle()
    {
        Code = null!;
        Plate = null!;
        Model = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a vehicle from a command.
    /// </summary>
    /// <param name="command">The command with the vehicle data.</param>
    public Vehicle(CreateVehicleCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Plate = command.Plate;
        Model = command.Model;
        Capacity = command.Capacity;
        Status = command.Status;
        CurrentLatitude = command.CurrentLatitude;
        CurrentLongitude = command.CurrentLongitude;
        LastPositionAt = LocalClock.Now();
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the unit code.
    /// </summary>
    public VehicleCode Code { get; private set; }

    /// <summary>
    ///     Gets the license plate.
    /// </summary>
    public LicensePlate Plate { get; private set; }

    /// <summary>
    ///     Gets the model.
    /// </summary>
    public string Model { get; private set; }

    /// <summary>
    ///     Gets the passenger capacity.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public VehicleStatus Status { get; private set; }

    /// <summary>
    ///     Gets the current latitude.
    /// </summary>
    public double CurrentLatitude { get; private set; }

    /// <summary>
    ///     Gets the current longitude.
    /// </summary>
    public double CurrentLongitude { get; private set; }

    /// <summary>
    ///     Gets the moment of the last reported position.
    /// </summary>
    public DateTime? LastPositionAt { get; private set; }

    /// <summary>
    ///     Updates the vehicle from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateVehicleCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var positionChanged = CurrentLatitude != command.CurrentLatitude ||
                              CurrentLongitude != command.CurrentLongitude;
        Code = command.Code;
        Plate = command.Plate;
        Model = command.Model;
        Capacity = command.Capacity;
        Status = command.Status;
        CurrentLatitude = command.CurrentLatitude;
        CurrentLongitude = command.CurrentLongitude;
        if (positionChanged) LastPositionAt = LocalClock.Now();
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Model, nameof(Model));
        Guard.NotLongerThan(Model, 60, nameof(Model));
        Guard.InRange(Capacity, 1, 200, nameof(Capacity));
        Guard.InRange(CurrentLatitude, -90.0, 90.0, nameof(CurrentLatitude));
        Guard.InRange(CurrentLongitude, -180.0, 180.0, nameof(CurrentLongitude));
    }
}
