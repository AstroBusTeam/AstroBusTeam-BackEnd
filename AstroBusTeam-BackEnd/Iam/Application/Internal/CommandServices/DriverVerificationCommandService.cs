using SecurityBus.API.Fleet.Interfaces.ACL;
using SecurityBus.API.Iam.Application.Errors;
using SecurityBus.API.Iam.Application.Services;
using SecurityBus.API.Iam.Domain.Model.Commands;
using SecurityBus.API.Iam.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Application service that verifies the identity of a driver.
/// </summary>
/// <remarks>
///     The service never touches Fleet's domain model: it talks to the Fleet bounded context through its
///     anti-corruption layer facade.
/// </remarks>
/// <param name="fleetContextFacade">Facade of the Fleet bounded context.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class DriverVerificationCommandService(
    IFleetContextFacade fleetContextFacade,
    ILogger<DriverVerificationCommandService> logger)
    : IDriverVerificationCommandService
{
    /// <inheritdoc />
    public async Task<Result<DriverIdentity, VerifyDriverError>> Handle(VerifyDriverCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var driver = await fleetContextFacade.FindDriverByEmployeeCodeAsync(command.EmployeeCode.Value, cancellationToken);
            if (driver is null)
            {
                logger.LogWarning("Identity verification failed: employee code {EmployeeCode} does not exist",
                    command.EmployeeCode);
                return new Result<DriverIdentity, VerifyDriverError>.Failure(VerifyDriverError.DriverNotFound);
            }

            if (!driver.IsActive)
            {
                logger.LogWarning("Identity verification failed: driver {DriverId} is not active", driver.Id);
                return new Result<DriverIdentity, VerifyDriverError>.Failure(VerifyDriverError.DriverNotActive);
            }

            var assignment = await fleetContextFacade.FindActiveAssignmentByDriverIdAsync(driver.Id, cancellationToken);
            if (assignment is null)
            {
                logger.LogWarning("Identity verification failed: driver {DriverId} has no active assignment", driver.Id);
                return new Result<DriverIdentity, VerifyDriverError>.Failure(VerifyDriverError.NoActiveAssignment);
            }

            var vehicle = await fleetContextFacade.FindVehicleByIdAsync(assignment.VehicleId, cancellationToken);
            if (vehicle is null)
            {
                logger.LogWarning("Identity verification failed: vehicle {VehicleId} does not exist", assignment.VehicleId);
                return new Result<DriverIdentity, VerifyDriverError>.Failure(VerifyDriverError.AssignedVehicleNotFound);
            }

            return new Result<DriverIdentity, VerifyDriverError>.Success(
                new DriverIdentity(driver.Id, driver.EmployeeCode, driver.FullName, vehicle.Id, vehicle.Code,
                    assignment.Route));
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while verifying a driver identity");
            return new Result<DriverIdentity, VerifyDriverError>.Failure(VerifyDriverError.UnexpectedError);
        }
    }
}
