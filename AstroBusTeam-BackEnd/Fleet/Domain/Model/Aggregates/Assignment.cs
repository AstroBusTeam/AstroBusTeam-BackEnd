using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents the assignment of a driver and a vehicle to a route
/// </summary>
/// <remarks>
///     <see cref="ActiveDriverId"/> and <see cref="ActiveVehicleId"/> are only filled while the assignment is
///     active. A unique index over them guarantees that a driver or a vehicle has a single active assignment.
/// </remarks>
public partial class Assignment
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Assignment()
    {
        Status = null!;
    }

    /// <summary>
    ///     Creates an assignment from a command.
    /// </summary>
    /// <param name="command">The command with the assignment data.</param>
    public Assignment(CreateAssignmentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        RouteId = command.RouteId;
        StartTime = command.StartTime;
        ValidFrom = command.ValidFrom;
        ValidTo = command.ValidTo;
        Status = command.Status;
        AssignedBy = command.AssignedBy;
        SyncActiveKeys();
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

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
    ///     Gets the daily start time.
    /// </summary>
    public TimeOnly StartTime { get; private set; }

    /// <summary>
    ///     Gets the first day the assignment is valid.
    /// </summary>
    public DateOnly ValidFrom { get; private set; }

    /// <summary>
    ///     Gets the last day the assignment is valid, if it has an end.
    /// </summary>
    public DateOnly? ValidTo { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public AssignmentStatus Status { get; private set; }

    /// <summary>
    ///     Gets the identifier of the user who made the assignment.
    /// </summary>
    public int? AssignedBy { get; private set; }

    /// <summary>
    ///     Gets the driver identifier while the assignment is active, otherwise null.
    /// </summary>
    public int? ActiveDriverId { get; private set; }

    /// <summary>
    ///     Gets the vehicle identifier while the assignment is active, otherwise null.
    /// </summary>
    public int? ActiveVehicleId { get; private set; }

    /// <summary>
    ///     Updates the status and the end date of the assignment from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateAssignmentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Status = command.Status;
        ValidTo = command.ValidTo;
        SyncActiveKeys();
        Validate();
    }

    private void SyncActiveKeys()
    {
        ActiveDriverId = Status.IsActive ? DriverId : null;
        ActiveVehicleId = Status.IsActive ? VehicleId : null;
    }

    private void Validate()
    {
        Guard.AtLeast(DriverId, 1, nameof(DriverId));
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        Guard.AtLeast(RouteId, 1, nameof(RouteId));
        if (AssignedBy is not null) Guard.AtLeast(AssignedBy.Value, 1, nameof(AssignedBy));
        if (ValidTo is not null && ValidTo < ValidFrom)
            throw new ArgumentException("ValidTo cannot be earlier than ValidFrom.", nameof(ValidTo));
    }
}
