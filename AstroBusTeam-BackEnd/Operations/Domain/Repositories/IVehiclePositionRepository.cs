using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Operations.Domain.Repositories;

/// <summary>
///     Repository for vehicle positions
/// </summary>
public interface IVehiclePositionRepository : IBaseRepository<VehiclePosition>
{
    /// <summary>
    ///     Finds vehicle positions, optionally filtered.
    /// </summary>
    Task<IEnumerable<VehiclePosition>> FindAllAsync(int? vehicleId = null, int? shiftId = null, CancellationToken cancellationToken = default);
}
