namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

/// <summary>
///     Represents the verified identity of a driver and the unit the driver is assigned to.
/// </summary>
/// <param name="DriverId">The driver identifier</param>
/// <param name="EmployeeCode">The employee code</param>
/// <param name="FullName">The first and last name of the driver</param>
/// <param name="VehicleId">The identifier of the assigned vehicle</param>
/// <param name="VehicleCode">The unit code of the assigned vehicle</param>
/// <param name="Route">The route of the active assignment</param>
public record DriverIdentityResource(
    int DriverId,
    string EmployeeCode,
    string FullName,
    int VehicleId,
    string VehicleCode,
    string Route);
