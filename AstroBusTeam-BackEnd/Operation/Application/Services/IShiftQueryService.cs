using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.Queries;

namespace SecurityBus.API.Operations.Application.Services;

/// <summary>
///     Query service interface for shift operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying shifts. It supports listing (with optional filters) and
///     retrieving a single shift by its identifier.
/// </remarks>
public interface IShiftQueryService
{
    /// <summary>
    ///     Handle the GetAllShiftsQuery.
    /// </summary>
    /// <param name="query">The GetAllShiftsQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Shift objects</returns>
    Task<IEnumerable<Shift>> Handle(GetAllShiftsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetShiftByIdQuery.
    /// </summary>
    /// <param name="query">The GetShiftByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Shift object if found, or null otherwise</returns>
    Task<Shift?> Handle(GetShiftByIdQuery query, CancellationToken cancellationToken = default);
}
