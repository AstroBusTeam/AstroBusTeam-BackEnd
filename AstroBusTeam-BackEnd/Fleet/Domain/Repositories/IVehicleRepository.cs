using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Domain.Repositories;

/// <summary>
///     The Vehicle repository contract.
/// </summary>
public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    /// <summary>
    ///     Find all vehicles, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Vehicle>> FindAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find a vehicle by unit code.
    /// </summary>
    Task<Vehicle?> FindByCodeAsync(VehicleCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find a vehicle by license plate.
    /// </summary>
    Task<Vehicle?> FindByPlateAsync(LicensePlate plate, CancellationToken cancellationToken = default);
}
