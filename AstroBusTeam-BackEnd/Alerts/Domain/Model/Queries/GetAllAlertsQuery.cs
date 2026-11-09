using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

/// <summary>
///     Query to get all alerts, optionally filtered
/// </summary>
/// <param name="Status">Optional filter by status</param>
public record GetAllAlertsQuery(AlertStatus? Status = null);
