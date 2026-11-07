namespace AstroBusTeamBackEnd.Operations.Domain.Model.Queries;

/// <summary>
///     Query to get a shift by ID
/// </summary>
/// <param name="Id">The shift ID to search</param>
public record GetShiftByIdQuery(int Id);
