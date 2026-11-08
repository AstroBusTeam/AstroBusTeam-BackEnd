using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;

/// <summary>
///     Represents a vehicle position of the SecurityBus platform
/// </summary>
public partial class VehiclePosition
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected VehiclePosition()
    {

    }

    /// <summary>
    ///     Creates a vehicle position from a command.
    /// </summary>
    /// <param name="command">The command with the vehicle position data.</param>
    public VehiclePosition(CreateVehiclePositionCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        VehicleId = command.VehicleId;
        ShiftId = command.ShiftId;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        SpeedKmh = command.SpeedKmh;
        RecordedAt = command.RecordedAt;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public int VehicleId { get; private set; }

    public int? ShiftId { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public double SpeedKmh { get; private set; }

    public DateTime RecordedAt { get; private set; }

    /// <summary>
    ///     Updates the vehicle position from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateVehiclePositionCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        VehicleId = command.VehicleId;
        ShiftId = command.ShiftId;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        SpeedKmh = command.SpeedKmh;
        RecordedAt = command.RecordedAt;
        Validate();
    }

    private void Validate()
    {
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        if (ShiftId is not null) Guard.AtLeast(ShiftId.Value, 1, nameof(ShiftId));
        Guard.InRange(Latitude, -90.0, 90.0, nameof(Latitude));
        Guard.InRange(Longitude, -180.0, 180.0, nameof(Longitude));
        Guard.AtLeast(SpeedKmh, 0.0, nameof(SpeedKmh));
    }
}
