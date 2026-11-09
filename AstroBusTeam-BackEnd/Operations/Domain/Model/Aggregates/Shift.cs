using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;

/// <summary>
///     Represents a shift driven by a driver with a vehicle on a route
/// </summary>
/// <remarks>
///     A shift belongs to an assignment and keeps the driver, vehicle and route of that assignment. A shift that is
///     not active must have an end time, and the average speed cannot exceed the maximum speed.
/// </remarks>
public partial class Shift
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Shift()
    {
        Status = null!;
    }

    /// <summary>
    ///     Creates a shift from a command.
    /// </summary>
    /// <param name="command">The command with the shift data.</param>
    public Shift(CreateShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        AssignmentId = command.AssignmentId;
        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        RouteId = command.RouteId;
        ShiftDate = command.ShiftDate;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
        DistanceKm = command.DistanceKm;
        Passengers = command.Passengers;
        Revenue = command.Revenue;
        Status = command.Status;
        AverageSpeedKmh = command.AverageSpeedKmh;
        MaxSpeedKmh = command.MaxSpeedKmh;
        OverspeedEvents = command.OverspeedEvents;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the identifier of the assignment.
    /// </summary>
    public int AssignmentId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the driver.
    /// </summary>
    public int DriverId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the vehicle.
    /// </summary>
    public int VehicleId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the route.
    /// </summary>
    public int RouteId { get; private set; }

    /// <summary>
    ///     Gets the date of the shift.
    /// </summary>
    public DateOnly ShiftDate { get; private set; }

    /// <summary>
    ///     Gets the start time.
    /// </summary>
    public DateTime StartTime { get; private set; }

    /// <summary>
    ///     Gets the end time, if the shift has ended.
    /// </summary>
    public DateTime? EndTime { get; private set; }

    /// <summary>
    ///     Gets the distance in kilometers.
    /// </summary>
    public double DistanceKm { get; private set; }

    /// <summary>
    ///     Gets the number of passengers.
    /// </summary>
    public int Passengers { get; private set; }

    /// <summary>
    ///     Gets the revenue.
    /// </summary>
    public decimal Revenue { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public ShiftStatus Status { get; private set; }

    /// <summary>
    ///     Gets the average speed in km/h.
    /// </summary>
    public double AverageSpeedKmh { get; private set; }

    /// <summary>
    ///     Gets the maximum speed in km/h.
    /// </summary>
    public double MaxSpeedKmh { get; private set; }

    /// <summary>
    ///     Gets the number of overspeed events.
    /// </summary>
    public int OverspeedEvents { get; private set; }

    /// <summary>
    ///     Updates the shift from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        AssignmentId = command.AssignmentId;
        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        RouteId = command.RouteId;
        ShiftDate = command.ShiftDate;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
        DistanceKm = command.DistanceKm;
        Passengers = command.Passengers;
        Revenue = command.Revenue;
        Status = command.Status;
        AverageSpeedKmh = command.AverageSpeedKmh;
        MaxSpeedKmh = command.MaxSpeedKmh;
        OverspeedEvents = command.OverspeedEvents;
        Validate();
    }

    private void Validate()
    {
        Guard.AtLeast(AssignmentId, 1, nameof(AssignmentId));
        Guard.AtLeast(DriverId, 1, nameof(DriverId));
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        Guard.AtLeast(RouteId, 1, nameof(RouteId));
        Guard.AtLeast(DistanceKm, 0.0, nameof(DistanceKm));
        Guard.AtLeast(Passengers, 0, nameof(Passengers));
        Guard.AtLeast(Revenue, 0m, nameof(Revenue));
        Guard.AtLeast(AverageSpeedKmh, 0.0, nameof(AverageSpeedKmh));
        Guard.AtLeast(MaxSpeedKmh, 0.0, nameof(MaxSpeedKmh));
        Guard.InRange(OverspeedEvents, 0, 32767, nameof(OverspeedEvents));

        if (EndTime is not null && EndTime < StartTime)
            throw new ArgumentException("EndTime cannot be earlier than StartTime.", nameof(EndTime));

        if (!Status.IsActive && EndTime is null)
            throw new ArgumentException("EndTime is required when the shift is not active.", nameof(EndTime));

        if (MaxSpeedKmh < AverageSpeedKmh)
            throw new ArgumentException("MaxSpeedKmh cannot be lower than AverageSpeedKmh.", nameof(MaxSpeedKmh));
    }
}
