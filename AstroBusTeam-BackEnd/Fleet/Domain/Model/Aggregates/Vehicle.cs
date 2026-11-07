using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a vehicle in the Fleet bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Vehicle
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Vehicle()
    {
        Code = null!;
        Plate = null!;
        Model = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Vehicle from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateVehicleCommand.
    /// </remarks>
    /// <param name="command">The CreateVehicleCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Vehicle(CreateVehicleCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Plate = command.Plate;
        Model = command.Model;
        Capacity = command.Capacity;
        Status = command.Status;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this vehicle.
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
    ///     Gets the vehicle status (available, in-service or maintenance).
    /// </summary>
    public VehicleStatus Status { get; private set; }

    /// <summary>
    ///     Gets the latitude.
    /// </summary>
    public double Latitude { get; private set; }

    /// <summary>
    ///     Gets the longitude.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    ///     Updates the vehicle with the values of an update command.
    /// </summary>
    /// <remarks>
    ///     This method acts as the command handler for UpdateVehicleCommand. The invariants are validated again.
    /// </remarks>
    /// <param name="command">The UpdateVehicleCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public void Update(UpdateVehicleCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Plate = command.Plate;
        Model = command.Model;
        Capacity = command.Capacity;
        Status = command.Status;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Model, nameof(Model));
        Guard.NotLongerThan(Model, 100, nameof(Model));
        Guard.InRange(Capacity, 1, 200, nameof(Capacity));
        Guard.InRange(Latitude, -90.0, 90.0, nameof(Latitude));
        Guard.InRange(Longitude, -180.0, 180.0, nameof(Longitude));
    }
}
