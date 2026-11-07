using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a delivery in the Alerts bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Delivery
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Delivery()
    {
        RecipientName = string.Empty;
        Channel = string.Empty;
        AlertType = string.Empty;
        Priority = string.Empty;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Delivery from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateDeliveryCommand.
    /// </remarks>
    /// <param name="command">The CreateDeliveryCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Delivery(CreateDeliveryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        RecipientName = command.RecipientName;
        Channel = command.Channel;
        AlertType = command.AlertType;
        Priority = command.Priority;
        Status = command.Status;
        SentAt = command.SentAt;
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this delivery.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the name of the recipient.
    /// </summary>
    public string RecipientName { get; private set; }

    /// <summary>
    ///     Gets the notification channel.
    /// </summary>
    public string Channel { get; private set; }

    /// <summary>
    ///     Gets the type of the alert delivered.
    /// </summary>
    public string AlertType { get; private set; }

    /// <summary>
    ///     Gets the message priority.
    /// </summary>
    public string Priority { get; private set; }

    /// <summary>
    ///     Gets the delivery status (sent or failed).
    /// </summary>
    public DeliveryStatus Status { get; private set; }

    /// <summary>
    ///     Gets the date and time of the delivery.
    /// </summary>
    public DateTime SentAt { get; private set; }

    private void Validate()
    {
        Guard.NotBlank(RecipientName, nameof(RecipientName));
        Guard.NotLongerThan(RecipientName, 100, nameof(RecipientName));
        Guard.NotBlank(Channel, nameof(Channel));
        Guard.NotLongerThan(Channel, 50, nameof(Channel));
        Guard.NotBlank(AlertType, nameof(AlertType));
        Guard.NotLongerThan(AlertType, 100, nameof(AlertType));
        Guard.NotBlank(Priority, nameof(Priority));
        Guard.NotLongerThan(Priority, 20, nameof(Priority));
    }
}
