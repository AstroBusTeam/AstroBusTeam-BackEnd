using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Repositories;

namespace SecurityBus.API.Fleet.Domain.Repositories;

/// <summary>
///     The Driver repository contract.
/// </summary>
public interface IDriverRepository : IBaseRepository<Driver>
{
    /// <summary>
    ///     Find all drivers, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Driver>> FindAllAsync(EmployeeCode? employeeCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find a driver by employee code.
    /// </summary>
    Task<Driver?> FindByEmployeeCodeAsync(EmployeeCode employeeCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find a driver by national ID (DNI).
    /// </summary>
    Task<Driver?> FindByDniAsync(Dni dni, CancellationToken cancellationToken = default);
}
