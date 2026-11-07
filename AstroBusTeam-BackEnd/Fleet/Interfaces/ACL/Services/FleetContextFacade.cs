using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.ACL.Services;

/// <summary>
///     Implementation of the Fleet anti-corruption layer facade.
/// </summary>
/// <param name="driverRepository">The driver repository</param>
/// <param name="vehicleRepository">The vehicle repository</param>
/// <param name="assignmentRepository">The assignment repository</param>
public class FleetContextFacade(
    IDriverRepository driverRepository,
    IVehicleRepository vehicleRepository,
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
    public async Task<AssignmentSummary?> FindActiveAssignmentByDriverIdAsync(int driverId,
        CancellationToken cancellationToken = default)
    {
        var assignment = await assignmentRepository.FindActiveByDriverIdAsync(driverId, cancellationToken);
        return assignment is null
            ? null
            : new AssignmentSummary(assignment.Id, assignment.DriverId, assignment.VehicleId, assignment.Route);
    }
}
