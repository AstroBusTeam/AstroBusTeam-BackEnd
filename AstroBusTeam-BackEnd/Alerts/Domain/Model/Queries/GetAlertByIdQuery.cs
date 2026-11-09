namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

/// <summary>
///     Query to get a alert by ID
/// </summary>
/// <param name="Id">The alert ID to search</param>
public record GetAlertByIdQuery(int Id);
