using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.ACL.Services;

/// <summary>
///     Implementation of the Fleet facade
/// </summary>
public class FleetContextFacade(
    IDriverRepository driverRepository,
    IVehicleRepository vehicleRepository,
    IBusRouteRepository routeRepository,
    IAssignmentRepository assignmentRepository) : IFleetContextFacade
{
    /// <inheritdoc />
    public async Task<DriverSummary?> FindDriverByIdAsync(int driverId, CancellationToken cancellationToken = default)
    {
        var driver = await driverRepository.FindByIdAsync(driverId, cancellationToken);
        return driver is null
            ? null
            : new DriverSummary(driver.Id, driver.EmployeeCode.Value, $"{driver.FirstName} {driver.LastName}", driver.Status.IsActive);
    }

    /// <inheritdoc />
    public async Task<DriverSummary?> FindDriverByEmployeeCodeAsync(string employeeCode,
        CancellationToken cancellationToken = default)
    {
        var driver = await driverRepository.FindByEmployeeCodeAsync(new EmployeeCode(employeeCode), cancellationToken);
        return driver is null
            ? null
            : new DriverSummary(driver.Id, driver.EmployeeCode.Value, $"{driver.FirstName} {driver.LastName}", driver.Status.IsActive);
    }

    /// <inheritdoc />
    public async Task<VehicleSummary?> FindVehicleByIdAsync(int vehicleId, CancellationToken cancellationToken = default)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId, cancellationToken);
        return vehicle is null ? null : new VehicleSummary(vehicle.Id, vehicle.Code.Value, vehicle.Plate.Value);
    }

    /// <inheritdoc />
    public async Task<RouteSummary?> FindRouteByIdAsync(int routeId, CancellationToken cancellationToken = default)
    {
        var route = await routeRepository.FindByIdAsync(routeId, cancellationToken);
        return route is null ? null : new RouteSummary(route.Id, route.Code, route.Name);
    }

    /// <inheritdoc />
    public async Task<AssignmentSummary?> FindAssignmentByIdAsync(int assignmentId,
        CancellationToken cancellationToken = default)
    {
        var assignment = await assignmentRepository.FindByIdAsync(assignmentId, cancellationToken);
        return assignment is null ? null : await ToSummaryAsync(assignment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AssignmentSummary?> FindActiveAssignmentByDriverIdAsync(int driverId,
        CancellationToken cancellationToken = default)
    {
        var assignment = await assignmentRepository.FindActiveByDriverIdAsync(driverId, cancellationToken);
        return assignment is null ? null : await ToSummaryAsync(assignment, cancellationToken);
    }

    private async Task<AssignmentSummary> ToSummaryAsync(Assignment assignment, CancellationToken cancellationToken)
    {
        var route = await routeRepository.FindByIdAsync(assignment.RouteId, cancellationToken);
        var routeText = route is null ? string.Empty : $"{route.Code} {route.Name}";
        return new AssignmentSummary(assignment.Id, assignment.DriverId, assignment.VehicleId, assignment.RouteId,
            routeText);
    }
}
