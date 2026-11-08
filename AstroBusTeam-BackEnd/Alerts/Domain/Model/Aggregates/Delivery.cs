using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents the delivery of an alert to a recipient through a notification channel
/// </summary>
public partial class Delivery
{
    /// <summary>
    ///     The priorities accepted for a delivery.
    /// </summary>
    public static readonly string[] Priorities = ["low", "medium", "high", "critical"];

    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Delivery()
    {
        Priority = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a delivery from a command.
    /// </summary>
    /// <param name="command">The command with the delivery data.</param>
    public Delivery(CreateDeliveryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        AlertId = command.AlertId;
        RecipientId = command.RecipientId;
        ChannelId = command.ChannelId;
        AlertTypeId = command.AlertTypeId;
        Priority = command.Priority.Trim().ToLowerInvariant();
        Status = command.Status;
        SentAt = command.SentAt;
        ErrorMessage = command.ErrorMessage;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the identifier of the alert.
    /// </summary>
    public int AlertId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the recipient.
    /// </summary>
    public int RecipientId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the notification channel.
    /// </summary>
    public int ChannelId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the alert type.
    /// </summary>
    public int AlertTypeId { get; private set; }

    /// <summary>
    ///     Gets the priority.
    /// </summary>
    public string Priority { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public DeliveryStatus Status { get; private set; }

    /// <summary>
    ///     Gets the moment the delivery was sent.
    /// </summary>
    public DateTime SentAt { get; private set; }

    /// <summary>
    ///     Gets the error message when the delivery failed.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    private void Validate()
    {
        Guard.AtLeast(AlertId, 1, nameof(AlertId));
        Guard.AtLeast(RecipientId, 1, nameof(RecipientId));
        Guard.AtLeast(ChannelId, 1, nameof(ChannelId));
        Guard.AtLeast(AlertTypeId, 1, nameof(AlertTypeId));
        Guard.OneOf(Priority, Priorities, nameof(Priority));
        if (ErrorMessage is not null) Guard.NotLongerThan(ErrorMessage, 255, nameof(ErrorMessage));
    }
}
