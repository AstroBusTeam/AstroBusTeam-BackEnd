using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;

/// <summary>
///     Represents a shift in the Operations bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Shift
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Shift()
    {
        RouteCode = string.Empty;
        RouteName = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Shift from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateShiftCommand.
    /// </remarks>
    /// <param name="command">The CreateShiftCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Shift(CreateShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        RouteCode = command.RouteCode;
        RouteName = command.RouteName;
        Date = command.Date;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
        DistanceKm = command.DistanceKm;
        Passengers = command.Passengers;
        Revenue = command.Revenue;
        Incidents = command.Incidents;
        Status = command.Status;
        SpeedKmh = command.SpeedKmh;
        MaxSpeedKmh = command.MaxSpeedKmh;
        AverageSpeedKmh = command.AverageSpeedKmh;
        SpeedSamples = command.SpeedSamples;
        OverspeedEvents = command.OverspeedEvents;
        Normalize();
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this shift.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the driver ID.
    /// </summary>
    public int DriverId { get; private set; }

    /// <summary>
    ///     Gets the vehicle ID.
    /// </summary>
    public int VehicleId { get; private set; }

    /// <summary>
    ///     Gets the route code.
    /// </summary>
    public string RouteCode { get; private set; }

    /// <summary>
    ///     Gets the route name.
    /// </summary>
    public string RouteName { get; private set; }

    /// <summary>
    ///     Gets the shift date.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    ///     Gets the start date and time.
    /// </summary>
    public DateTime StartTime { get; private set; }

    /// <summary>
    ///     Gets the end date and time (null while the shift is active).
    /// </summary>
    public DateTime? EndTime { get; private set; }

    /// <summary>
    ///     Gets the distance traveled in km.
    /// </summary>
    public double DistanceKm { get; private set; }

    /// <summary>
    ///     Gets the passengers transported.
    /// </summary>
    public int Passengers { get; private set; }

    /// <summary>
    ///     Gets the revenue collected.
    /// </summary>
    public decimal Revenue { get; private set; }

    /// <summary>
    ///     Gets the incidents registered.
    /// </summary>
    public int Incidents { get; private set; }

    /// <summary>
    ///     Gets the shift status (active, finished or interrupted).
    /// </summary>
    public ShiftStatus Status { get; private set; }

    /// <summary>
    ///     Gets the current speed in km/h (0 when the shift ends).
    /// </summary>
    public double SpeedKmh { get; private set; }

    /// <summary>
    ///     Gets the maximum speed in km/h.
    /// </summary>
    public double MaxSpeedKmh { get; private set; }

    /// <summary>
    ///     Gets the average speed in km/h.
    /// </summary>
    public double AverageSpeedKmh { get; private set; }

    /// <summary>
    ///     Gets the number of speed samples taken.
    /// </summary>
    public int SpeedSamples { get; private set; }

    /// <summary>
    ///     Gets the times the speed limit was exceeded.
    /// </summary>
    public int OverspeedEvents { get; private set; }

    /// <summary>
    ///     Updates the shift with the values of an update command.
    /// </summary>
    /// <remarks>
    ///     This method acts as the command handler for UpdateShiftCommand. The invariants are validated again.
    /// </remarks>
    /// <param name="command">The UpdateShiftCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public void Update(UpdateShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        RouteCode = command.RouteCode;
        RouteName = command.RouteName;
        Date = command.Date;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
        DistanceKm = command.DistanceKm;
        Passengers = command.Passengers;
        Revenue = command.Revenue;
        Incidents = command.Incidents;
        Status = command.Status;
        SpeedKmh = command.SpeedKmh;
        MaxSpeedKmh = command.MaxSpeedKmh;
        AverageSpeedKmh = command.AverageSpeedKmh;
        SpeedSamples = command.SpeedSamples;
        OverspeedEvents = command.OverspeedEvents;
        Normalize();
        Validate();
    }

    private void Normalize()
    {
        if (!Status.IsActive) SpeedKmh = 0;
    }

    private void Validate()
    {
        Guard.AtLeast(DriverId, 1, nameof(DriverId));
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        Guard.NotBlank(RouteCode, nameof(RouteCode));
        Guard.NotLongerThan(RouteCode, 20, nameof(RouteCode));
        Guard.NotBlank(RouteName, nameof(RouteName));
        Guard.NotLongerThan(RouteName, 150, nameof(RouteName));
        Guard.AtLeast(DistanceKm, 0.0, nameof(DistanceKm));
        Guard.AtLeast(Passengers, 0, nameof(Passengers));
        Guard.AtLeast(Revenue, 0m, nameof(Revenue));
        Guard.AtLeast(Incidents, 0, nameof(Incidents));
        Guard.AtLeast(SpeedKmh, 0.0, nameof(SpeedKmh));
        Guard.AtLeast(MaxSpeedKmh, 0.0, nameof(MaxSpeedKmh));
        Guard.AtLeast(AverageSpeedKmh, 0.0, nameof(AverageSpeedKmh));
        Guard.AtLeast(SpeedSamples, 0, nameof(SpeedSamples));
        Guard.AtLeast(OverspeedEvents, 0, nameof(OverspeedEvents));
        if (EndTime is not null && EndTime < StartTime)
                throw new ArgumentException("EndTime cannot be earlier than StartTime.", nameof(EndTime));
        if (!Status.IsActive && EndTime is null)
                throw new ArgumentException("EndTime is required when the shift is not active.", nameof(EndTime));
        if (MaxSpeedKmh < SpeedKmh)
                throw new ArgumentException("MaxSpeedKmh cannot be lower than SpeedKmh.", nameof(MaxSpeedKmh));
    }
}
