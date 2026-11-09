using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Query service interface for driver operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying drivers. It supports listing (with optional filters) and
///     retrieving a single driver by its identifier.
/// </remarks>
public interface IDriverQueryService
{
    /// <summary>
    ///     Handle the GetAllDriversQuery.
    /// </summary>
    /// <param name="query">The GetAllDriversQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Driver objects</returns>
    Task<IEnumerable<Driver>> Handle(GetAllDriversQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetDriverByIdQuery.
    /// </summary>
    /// <param name="query">The GetDriverByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Driver object if found, or null otherwise</returns>
    Task<Driver?> Handle(GetDriverByIdQuery query, CancellationToken cancellationToken = default);
}
