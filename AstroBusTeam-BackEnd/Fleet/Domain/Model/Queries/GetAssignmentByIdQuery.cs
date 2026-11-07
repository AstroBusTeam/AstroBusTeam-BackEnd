namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

/// <summary>
///     Query to get a assignment by ID
/// </summary>
/// <param name="Id">The assignment ID to search</param>
public record GetAssignmentByIdQuery(int Id);
