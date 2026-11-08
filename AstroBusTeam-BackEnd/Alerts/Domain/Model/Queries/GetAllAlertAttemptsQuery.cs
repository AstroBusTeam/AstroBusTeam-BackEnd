namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

/// <summary>
///     Query to list alert attempts
/// </summary>
public record GetAllAlertAttemptsQuery(int? AlertId = null);
