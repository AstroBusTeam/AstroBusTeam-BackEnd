namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a alert attempt
/// </summary>
public record CreateAlertAttemptCommand(int AlertId, int AttemptNumber, DateTime SentAt, string Result);
