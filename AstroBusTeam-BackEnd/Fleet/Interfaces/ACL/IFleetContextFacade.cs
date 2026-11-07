namespace SecurityBus.API.Fleet.Interfaces.ACL;

/// <summary>
///     Summary of a driver exposed to other bounded contexts.
/// </summary>
/// <param name="Id">The driver identifier</param>
/// <param name="EmployeeCode">The employee code</param>
/// <param name="FullName">The first and last name</param>
/// <param name="IsActive">Whether the driver is active</param>
public record DriverSummary(int Id, string EmployeeCode, string FullName, bool IsActive);

/// <summary>
///     Summary of a vehicle exposed to other bounded contexts.
/// </summary>
/// <param name="Id">The vehicle identifier</param>
/// <param name="Code">The unit code</param>
/// <param name="Plate">The license plate</param>
public record VehicleSummary(int Id, string Code, string Plate);

/// <summary>
///     Summary of an assignment exposed to other bounded contexts.
/// </summary>
/// <param name="Id">The assignment identifier</param>
/// <param name="DriverId">The driver identifier</param>
/// <param name="VehicleId">The vehicle identifier</param>
/// <param name="Route">The assigned route</param>
public record AssignmentSummary(int Id, int DriverId, int VehicleId, string Route);

/// <summary>
///     Anti-corruption layer facade of the Fleet bounded context.
/// </summary>
/// <remarks>
///     Other bounded contexts (IAM, Operations and Alerts) use this facade instead of Fleet's repositories or
///     aggregates, so they only depend on plain summaries and never on Fleet's domain model.
/// </remarks>
public interface IFleetContextFacade
{
    /// <summary>
    ///     Finds a driver by identifier.
    /// </summary>
    /// <param name="driverId">The driver identifier</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The driver summary if found, or null otherwise.</returns>
    Task<DriverSummary?> FindDriverByIdAsync(int driverId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a driver by employee code.
    /// </summary>
    /// <param name="employeeCode">The raw employee code</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The driver summary if found, or null otherwise.</returns>
    Task<DriverSummary?> FindDriverByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a vehicle by identifier.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The vehicle summary if found, or null otherwise.</returns>
    Task<VehicleSummary?> FindVehicleByIdAsync(int vehicleId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds the active assignment of a driver.
    /// </summary>
    /// <param name="driverId">The driver identifier</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The assignment summary if the driver has an active assignment, or null otherwise.</returns>
    Task<AssignmentSummary?> FindActiveAssignmentByDriverIdAsync(int driverId, CancellationToken cancellationToken = default);
}
