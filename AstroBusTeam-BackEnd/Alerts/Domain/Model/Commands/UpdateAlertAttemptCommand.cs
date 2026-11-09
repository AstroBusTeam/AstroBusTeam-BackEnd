namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a alert attempt
/// </summary>
public record UpdateAlertAttemptCommand(int Id, int AlertId, int AttemptNumber, DateTime SentAt, string Result);
