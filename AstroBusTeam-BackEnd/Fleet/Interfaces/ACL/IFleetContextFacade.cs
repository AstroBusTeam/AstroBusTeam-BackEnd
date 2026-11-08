namespace AstroBusTeamBackEnd.Fleet.Interfaces.ACL;

/// <summary>
///     Summary of a driver exposed to other bounded contexts
/// </summary>
/// <param name="Id">The identifier of the driver</param>
/// <param name="EmployeeCode">The employee code</param>
/// <param name="FullName">The full name</param>
/// <param name="IsActive">Whether the driver is active</param>
public record DriverSummary(int Id, string EmployeeCode, string FullName, bool IsActive);

/// <summary>
///     Summary of a vehicle exposed to other bounded contexts
/// </summary>
/// <param name="Id">The identifier of the vehicle</param>
/// <param name="Code">The unit code</param>
/// <param name="Plate">The license plate</param>
public record VehicleSummary(int Id, string Code, string Plate);

/// <summary>
///     Summary of a route exposed to other bounded contexts
/// </summary>
/// <param name="Id">The identifier of the route</param>
/// <param name="Code">The route code</param>
/// <param name="Name">The route name</param>
public record RouteSummary(int Id, string Code, string Name);

/// <summary>
///     Summary of an assignment exposed to other bounded contexts
/// </summary>
/// <param name="Id">The identifier of the assignment</param>
/// <param name="DriverId">The identifier of the driver</param>
/// <param name="VehicleId">The identifier of the vehicle</param>
/// <param name="RouteId">The identifier of the route</param>
/// <param name="Route">The route code and name</param>
public record AssignmentSummary(int Id, int DriverId, int VehicleId, int RouteId, string Route);

/// <summary>
///     Anti-corruption layer facade of the Fleet bounded context
/// </summary>
public interface IFleetContextFacade
{
    /// <summary>
    ///     Finds a driver by identifier.
    /// </summary>
    Task<DriverSummary?> FindDriverByIdAsync(int driverId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a driver by employee code.
    /// </summary>
    Task<DriverSummary?> FindDriverByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a vehicle by identifier.
    /// </summary>
    Task<VehicleSummary?> FindVehicleByIdAsync(int vehicleId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a route by identifier.
    /// </summary>
    Task<RouteSummary?> FindRouteByIdAsync(int routeId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds an assignment by identifier.
    /// </summary>
    Task<AssignmentSummary?> FindAssignmentByIdAsync(int assignmentId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds the active assignment of a driver.
    /// </summary>
    Task<AssignmentSummary?> FindActiveAssignmentByDriverIdAsync(int driverId, CancellationToken cancellationToken = default);
}
