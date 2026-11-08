using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Application.Extensions;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.CommandServices;

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
    IBusRouteRepository routeRepository,
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

        if (await routeRepository.FindByIdAsync(command.RouteId, cancellationToken) is null)
        {
            logger.LogWarning(
                "Assignment rejected because route {RouteId} does not exist",
                command.RouteId);
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.RouteNotFound);
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
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate active assignment while creating assignment");
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.DriverAlreadyAssigned);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating assignment");
            return new Result<Assignment, CreateAssignmentError>.Failure(
                CreateAssignmentError.InvalidReference);
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
    public async Task<Result<Assignment, UpdateAssignmentError>> Handle(UpdateAssignmentCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await assignmentRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Assignment {Id} was not found for update", command.Id);
            return new Result<Assignment, UpdateAssignmentError>.Failure(
                UpdateAssignmentError.AssignmentNotFound);
        }

        if (command.Status.IsActive)
        {
            var driverActive = await assignmentRepository.FindActiveByDriverIdAsync(entity.DriverId, cancellationToken);
            if (driverActive is not null && driverActive.Id != entity.Id)
                return new Result<Assignment, UpdateAssignmentError>.Failure(
                    UpdateAssignmentError.DriverAlreadyAssigned);

            var vehicleActive = await assignmentRepository.FindActiveByVehicleIdAsync(entity.VehicleId, cancellationToken);
            if (vehicleActive is not null && vehicleActive.Id != entity.Id)
                return new Result<Assignment, UpdateAssignmentError>.Failure(
                    UpdateAssignmentError.VehicleAlreadyAssigned);
        }

        try
        {
            entity.Update(command);
            assignmentRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Assignment, UpdateAssignmentError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating assignment");
            return new Result<Assignment, UpdateAssignmentError>.Failure(
                UpdateAssignmentError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate active assignment while updating assignment");
            return new Result<Assignment, UpdateAssignmentError>.Failure(
                UpdateAssignmentError.DriverAlreadyAssigned);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating assignment");
            return new Result<Assignment, UpdateAssignmentError>.Failure(
                UpdateAssignmentError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating assignment");
            return new Result<Assignment, UpdateAssignmentError>.Failure(
                UpdateAssignmentError.UnexpectedError);
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
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Assignment {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<Assignment, DeleteAssignmentError>.Failure(
                DeleteAssignmentError.AssignmentHasRelatedData);
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
