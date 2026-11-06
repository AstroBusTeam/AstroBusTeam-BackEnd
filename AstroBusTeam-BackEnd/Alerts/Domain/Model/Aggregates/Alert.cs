using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Model;

namespace SecurityBus.API.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a alert in the Alerts bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Alert
{
    /// <summary>
    ///     Maximum number of delivery attempts before an alert becomes critical.
    /// </summary>
    public const int MaxAttempts = 3;

    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Alert()
    {
        Code = null!;
        Type = string.Empty;
        Receiver = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Alert from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateAlertCommand.
    /// </remarks>
    /// <param name="command">The CreateAlertCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Alert(CreateAlertCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        VehicleId = command.VehicleId;
        Type = command.Type;
        Receiver = command.Receiver;
        Attempts = command.Attempts;
        Status = command.Status;
        OccurredAt = command.OccurredAt;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        Normalize();
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this alert.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the alert code (generated when omitted).
    /// </summary>
    public AlertCode Code { get; private set; }

    /// <summary>
    ///     Gets the vehicle ID.
    /// </summary>
    public int VehicleId { get; private set; }

    /// <summary>
    ///     Gets the alert type.
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    ///     Gets the receiver of the alert.
    /// </summary>
    public string Receiver { get; private set; }

    /// <summary>
    ///     Gets the delivery attempts (1 to 3).
    /// </summary>
    public int Attempts { get; private set; }

    /// <summary>
    ///     Gets the alert status (pending, confirmed or critical).
    /// </summary>
    public AlertStatus Status { get; private set; }

    /// <summary>
    ///     Gets the date and time when the alert occurred.
    /// </summary>
    public DateTime OccurredAt { get; private set; }

    /// <summary>
    ///     Gets the latitude.
    /// </summary>
    public double Latitude { get; private set; }

    /// <summary>
    ///     Gets the longitude.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    ///     Updates the alert with the values of an update command.
    /// </summary>
    /// <remarks>
    ///     This method acts as the command handler for UpdateAlertCommand. The invariants are validated again.
    /// </remarks>
    /// <param name="command">The UpdateAlertCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public void Update(UpdateAlertCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        VehicleId = command.VehicleId;
        Type = command.Type;
        Receiver = command.Receiver;
        Attempts = command.Attempts;
        Status = command.Status;
        OccurredAt = command.OccurredAt;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        Normalize();
        Validate();
    }

    private void Normalize()
    {
        if (Attempts >= MaxAttempts && Status.IsPending) Status = AlertStatus.Critical;
    }

    private void Validate()
    {
        Guard.AtLeast(VehicleId, 1, nameof(VehicleId));
        Guard.NotBlank(Type, nameof(Type));
        Guard.NotLongerThan(Type, 100, nameof(Type));
        Guard.NotBlank(Receiver, nameof(Receiver));
        Guard.NotLongerThan(Receiver, 100, nameof(Receiver));
        Guard.InRange(Attempts, 1, 3, nameof(Attempts));
        Guard.InRange(Latitude, -90.0, 90.0, nameof(Latitude));
        Guard.InRange(Longitude, -180.0, 180.0, nameof(Longitude));
    }
}
