namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

/// <summary>
///     Query to get a driver by ID
/// </summary>
/// <param name="Id">The driver ID to search</param>
public record GetDriverByIdQuery(int Id);
