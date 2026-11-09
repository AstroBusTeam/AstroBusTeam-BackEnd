using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a notification channel of the SecurityBus platform
/// </summary>
public partial class NotificationChannel
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected NotificationChannel()
    {
        Code = string.Empty;
    }

    /// <summary>
    ///     Creates a notification channel from a command.
    /// </summary>
    /// <param name="command">The command with the notification channel data.</param>
    public NotificationChannel(CreateNotificationChannelCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code.Trim().ToLowerInvariant();
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public string Code { get; private set; }

    /// <summary>
    ///     Updates the notification channel from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateNotificationChannelCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code.Trim().ToLowerInvariant();
        Validate();
    }

    private void Validate()
    {
        Guard.OneOf(Code, ["sms", "push", "email", "web", "call"], nameof(Code));
    }
}
