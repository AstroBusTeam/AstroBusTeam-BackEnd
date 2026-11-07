using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Domain.Model.Queries;

namespace SecurityBus.API.Fleet.Application.Services;

/// <summary>
///     Query service interface for assignment operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying assignments. It supports listing (with optional filters) and
///     retrieving a single assignment by its identifier.
/// </remarks>
public interface IAssignmentQueryService
{
    /// <summary>
    ///     Handle the GetAllAssignmentsQuery.
    /// </summary>
    /// <param name="query">The GetAllAssignmentsQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Assignment objects</returns>
    Task<IEnumerable<Assignment>> Handle(GetAllAssignmentsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetAssignmentByIdQuery.
    /// </summary>
    /// <param name="query">The GetAssignmentByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Assignment object if found, or null otherwise</returns>
    Task<Assignment?> Handle(GetAssignmentByIdQuery query, CancellationToken cancellationToken = default);
}
