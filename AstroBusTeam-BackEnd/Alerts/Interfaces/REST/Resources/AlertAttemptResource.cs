namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a alert attempt
/// </summary>
public record AlertAttemptResource(
    int Id,
    int AlertId,
    int AttemptNumber,
    DateTime SentAt,
    string Result);
