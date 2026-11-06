namespace SecurityBus.API.Alerts.Domain.Model.Queries;

/// <summary>
///     Query to get a recipient by ID
/// </summary>
/// <param name="Id">The recipient ID to search</param>
public record GetRecipientByIdQuery(int Id);
