using SecurityBus.API.Fleet.Application.Errors;
using SecurityBus.API.Fleet.Application.Services;
using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Domain.Model.Commands;
using SecurityBus.API.Fleet.Domain.Repositories;
using SecurityBus.API.Shared.Application.Extensions;
using SecurityBus.API.Shared.Application.Patterns;
using SecurityBus.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Fleet.Application.Internal.CommandServices;

/// <summary>
///     Application service for handling assignment commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the assignment aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="assignmentRepository">Repository for accessing assignment data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class AssignmentCommandService(
    IAssignmentRepository assignmentRepository,
    IDriverRepository driverRepository,
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork,
    ILogger<AssignmentCommandService> logger)
    : IAssignmentCommandService
{
    /// <inheritdoc />
    public async Task<Result<Assignment, CreateAssignmentError>> Handle(CreateAssignmentCommand command,
        CancellationToken cancellationToken = default)
    {
        var driver = await driverRepository.FindByIdAsync(command.DriverId, cancellationToken);
        if (driver is null)
        {
            logger.LogWarning(
                "Assignment rejected because driver {DriverId} does not exist",
                command.DriverId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.DriverNotFound);
        }

        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId, cancellationToken);
        if (vehicle is null)
        {
            logger.LogWarning(
                "Assignment rejected because vehicle {VehicleId} does not exist",
                command.VehicleId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.VehicleNotFound);
        }

        if (!driver.Status.IsActive)
        {
            logger.LogWarning(
                "Assignment rejected because driver {DriverId} is not active",
                command.DriverId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.DriverNotActive);
        }

        if (vehicle.Status.IsMaintenance)
        {
            logger.LogWarning(
                "Assignment rejected because vehicle {VehicleId} is in maintenance",
                command.VehicleId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.VehicleInMaintenance);
        }

        if (command.Status.IsActive && await assignmentRepository.FindActiveByDriverIdAsync(command.DriverId, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Assignment rejected because driver {DriverId} already has an active assignment",
                command.DriverId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.DriverAlreadyAssigned);
        }

        if (command.Status.IsActive && await assignmentRepository.FindActiveByVehicleIdAsync(command.VehicleId, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Assignment rejected because vehicle {VehicleId} already has an active assignment",
                command.VehicleId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.VehicleAlreadyAssigned);
        }

        try
        {
            var entity = new Assignment(command);
            await assignmentRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Assignment, CreateAssignmentError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating assignment");
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.InvalidData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating assignment");
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating assignment");
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Assignment, DeleteAssignmentError>> Handle(DeleteAssignmentCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await assignmentRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Assignment {Id} was not found for deletion", command.Id);
            return new Result<Assignment, DeleteAssignmentError>.Failure(
                DeleteAssignmentError.AssignmentNotFound);
        }


        try
        {
            assignmentRepository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Assignment, DeleteAssignmentError>.Success(entity);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting assignment");
            return new Result<Assignment, DeleteAssignmentError>.Failure(
                DeleteAssignmentError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting assignment");
            return new Result<Assignment, DeleteAssignmentError>.Failure(
                DeleteAssignmentError.UnexpectedError);
        }
    }
}
