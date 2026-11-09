using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a alert attempt
/// </summary>
public record UpdateAlertAttemptResource(
    int AlertId,
    int AttemptNumber,
    DateTime SentAt,
    [Required(ErrorMessage = "RequiredAlertAttemptResult")]
    string Result);
