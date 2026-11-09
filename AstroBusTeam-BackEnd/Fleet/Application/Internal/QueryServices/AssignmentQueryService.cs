using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying assignments.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving assignments. All queries delegate to the repository layer.
/// </remarks>
/// <param name="assignmentRepository">Repository for accessing assignment data.</param>
public class AssignmentQueryService(IAssignmentRepository assignmentRepository) : IAssignmentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Assignment>> Handle(GetAllAssignmentsQuery query, CancellationToken cancellationToken = default) =>
        await assignmentRepository.FindAllAsync(query.DriverId, query.Status, cancellationToken);

    /// <inheritdoc />
    public async Task<Assignment?> Handle(GetAssignmentByIdQuery query, CancellationToken cancellationToken = default) =>
        await assignmentRepository.FindByIdAsync(query.Id, cancellationToken);
}
