using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a alert attempt of the SecurityBus platform
/// </summary>
public partial class AlertAttempt
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected AlertAttempt()
    {
        Result = string.Empty;
    }

    /// <summary>
    ///     Creates a alert attempt from a command.
    /// </summary>
    /// <param name="command">The command with the alert attempt data.</param>
    public AlertAttempt(CreateAlertAttemptCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        AlertId = command.AlertId;
        AttemptNumber = command.AttemptNumber;
        SentAt = command.SentAt;
        Result = command.Result.Trim().ToLowerInvariant();
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public int AlertId { get; private set; }

    public int AttemptNumber { get; private set; }

    public DateTime SentAt { get; private set; }

    public string Result { get; private set; }

    /// <summary>
    ///     Updates the alert attempt from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateAlertAttemptCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        AlertId = command.AlertId;
        AttemptNumber = command.AttemptNumber;
        SentAt = command.SentAt;
        Result = command.Result.Trim().ToLowerInvariant();
        Validate();
    }

    private void Validate()
    {
        Guard.AtLeast(AlertId, 1, nameof(AlertId));
        Guard.InRange(AttemptNumber, 1, 10, nameof(AttemptNumber));
        Guard.OneOf(Result, ["success", "failed", "no_answer"], nameof(Result));
    }
}
