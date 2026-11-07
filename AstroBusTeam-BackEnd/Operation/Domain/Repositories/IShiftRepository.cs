using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Operations.Domain.Repositories;

/// <summary>
///     The Shift repository contract.
/// </summary>
public interface IShiftRepository : IBaseRepository<Shift>
{
    /// <summary>
    ///     Find all shifts, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Shift>> FindAllAsync(int? driverId = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find the active shift of a driver.
    /// </summary>
    Task<Shift?> FindActiveByDriverIdAsync(int driverId, CancellationToken cancellationToken = default);
}
