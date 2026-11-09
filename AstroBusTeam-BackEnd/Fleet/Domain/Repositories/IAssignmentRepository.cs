using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Domain.Repositories;

/// <summary>
///     The Assignment repository contract.
/// </summary>
public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    /// <summary>
    ///     Find all assignments, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Assignment>> FindAllAsync(int? driverId = null, AssignmentStatus? status = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find the active assignment of a driver.
    /// </summary>
    Task<Assignment?> FindActiveByDriverIdAsync(int driverId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find the active assignment of a vehicle.
    /// </summary>
    Task<Assignment?> FindActiveByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);
}
