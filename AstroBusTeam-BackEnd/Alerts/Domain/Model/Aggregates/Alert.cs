using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents an alert raised by a vehicle
/// </summary>
/// <remarks>
///     An alert can be tied to a driver and a shift, has a type and a receiver, and is confirmed by a user. When the
///     number of attempts reaches <see cref="MaxAttempts"/>, a pending alert becomes critical.
/// </remarks>
public partial class Alert
{
    /// <summary>
    ///     The maximum number of delivery attempts of an alert.
    /// </summary>
    public const int MaxAttempts = 3;

    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Alert()
    {
        Code = null!;
        Status = null!;
    }

    /// <summary>
    ///     Creates an alert from a command.
    /// </summary>
    /// <param name="command">The command with the alert data.</param>
    public Alert(CreateAlertCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        VehicleId = command.VehicleId;
        DriverId = command.DriverId;
        ShiftId = command.ShiftId;
        AlertTypeId = command.AlertTypeId;
        ReceiverId = command.ReceiverId;
        Attempts = command.Attempts;
        Status = command.Status;
        CreatedAt = command.CreatedAt;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        ConfirmedAt = command.ConfirmedAt;
        ConfirmedBy = command.ConfirmedBy;
        Notes = command.Notes;
        Normalize();
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the alert code.
    /// </summary>
    public AlertCode Code { get; private set; }

    /// <summary>
    ///     Gets the identifier of the vehicle that raised the alert.
    /// </summary>
    public int VehicleId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the driver, if known.
    /// </summary>
    public int? DriverId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the shift, if known.
    /// </summary>
    public int? ShiftId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the alert type.
    /// </summary>
    public int AlertTypeId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the recipient that receives the alert.
    /// </summary>
    public int ReceiverId { get; private set; }

    /// <summary>
    ///     Gets the number of delivery attempts.
    /// </summary>
    public int Attempts { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public AlertStatus Status { get; private set; }

    /// <summary>
    ///     Gets the moment the alert was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Gets the latitude where the alert was raised.
    /// </summary>
    public double Latitude { get; private set; }

    /// <summary>
    ///     Gets the longitude where the alert was raised.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    ///     Gets the moment the alert was confirmed, if it was.
    /// </summary>
    public DateTime? ConfirmedAt { get; private set; }

    /// <summary>
    ///     Gets the identifier of the user who confirmed the alert, if it was confirmed.
    /// </summary>
    public int? ConfirmedBy { get; private set; }

    /// <summary>
    ///     Gets the notes.
    /// </summary>
    public string? Notes { get; private set; }

    /// <summary>
    ///     Updates the alert from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateAlertCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        VehicleId = command.VehicleId;
        DriverId = command.DriverId;
        ShiftId = command.ShiftId;
        AlertTypeId = command.AlertTypeId;
        ReceiverId = command.ReceiverId;
        Attempts = command.Attempts;
        Status = command.Status;
        CreatedAt = command.CreatedAt;
        Latitude = command.Latitude;
        Longitude = command.Longitude;
        ConfirmedAt = command.ConfirmedAt;
        ConfirmedBy = command.ConfirmedBy;
        Notes = command.Notes;
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
        if (DriverId is not null) Guard.AtLeast(DriverId.Value, 1, nameof(DriverId));
        if (ShiftId is not null) Guard.AtLeast(ShiftId.Value, 1, nameof(ShiftId));
        Guard.AtLeast(AlertTypeId, 1, nameof(AlertTypeId));
        Guard.AtLeast(ReceiverId, 1, nameof(ReceiverId));
        if (ConfirmedBy is not null) Guard.AtLeast(ConfirmedBy.Value, 1, nameof(ConfirmedBy));
        Guard.InRange(Attempts, 1, MaxAttempts, nameof(Attempts));
        Guard.InRange(Latitude, -90.0, 90.0, nameof(Latitude));
        Guard.InRange(Longitude, -180.0, 180.0, nameof(Longitude));
    }
}
