using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a assignment in the Fleet bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Assignment
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Assignment()
    {
        Route = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Assignment from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateAssignmentCommand.
    /// </remarks>
    /// <param name="command">The CreateAssignmentCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Assignment(CreateAssignmentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        DriverId = command.DriverId;
        VehicleId = command.VehicleId;
        Route = command.Route;
        StartTime = command.StartTime;
        Status = command.Status;
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this assignment.
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
    ///     Gets the route.
    /// </summary>
    public string Route { get; private set; }

    /// <summary>
    ///     Gets the start time (HH:mm).
    /// </summary>
    public TimeOnly StartTime { get; private set; }

    /// <summary>
    ///     Gets the assignment status (active or inactive).
    /// </summary>
    public AssignmentStatus Status { get; private set; }

    private void Validate()
    {
        Guard.AtLeast(DriverId, 1, nameof(DriverId));
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        Guard.NotBlank(Route, nameof(Route));
        Guard.NotLongerThan(Route, 150, nameof(Route));
    }
}
