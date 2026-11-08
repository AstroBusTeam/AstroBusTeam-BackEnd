using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a alert attempt
/// </summary>
public record CreateAlertAttemptResource(
    int AlertId,
    int AttemptNumber,
    DateTime? SentAt = null,
    string? Result = null);
