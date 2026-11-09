namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

/// <summary>
///     Query to get a delivery by ID
/// </summary>
/// <param name="Id">The delivery ID to search</param>
public record GetDeliveryByIdQuery(int Id);
