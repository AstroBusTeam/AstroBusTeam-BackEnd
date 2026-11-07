using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Fleet.Application.Services;

/// <summary>
///     Query service interface for vehicle operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying vehicles. It supports listing (with optional filters) and
///     retrieving a single vehicle by its identifier.
/// </remarks>
public interface IVehicleQueryService
{
    /// <summary>
    ///     Handle the GetAllVehiclesQuery.
    /// </summary>
    /// <param name="query">The GetAllVehiclesQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Vehicle objects</returns>
    Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetVehicleByIdQuery.
    /// </summary>
    /// <param name="query">The GetVehicleByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Vehicle object if found, or null otherwise</returns>
    Task<Vehicle?> Handle(GetVehicleByIdQuery query, CancellationToken cancellationToken = default);
}
