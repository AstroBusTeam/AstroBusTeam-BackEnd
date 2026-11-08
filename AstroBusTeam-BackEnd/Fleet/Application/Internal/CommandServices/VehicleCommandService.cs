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
///     Application service for handling vehicle commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the vehicle aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="vehicleRepository">Repository for accessing vehicle data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class VehicleCommandService(
    IVehicleRepository vehicleRepository,
    IAssignmentRepository assignmentRepository,
    IUnitOfWork unitOfWork,
    ILogger<VehicleCommandService> logger)
    : IVehicleCommandService
{
    /// <inheritdoc />
    public async Task<Result<Vehicle, CreateVehicleError>> Handle(CreateVehicleCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await vehicleRepository.FindByCodeAsync(command.Code, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Duplicate unit code rejected for vehicle {Code}",
                command.Code);
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.DuplicateVehicleCode);
        }

        if (await vehicleRepository.FindByPlateAsync(command.Plate, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Duplicate license plate rejected for vehicle {Plate}",
                command.Plate);
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.DuplicateLicensePlate);
        }

        try
        {
            var entity = new Vehicle(command);
            await vehicleRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Vehicle, CreateVehicleError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating vehicle");
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating vehicle");
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.DuplicateVehicleCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating vehicle");
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating vehicle");
            return new Result<Vehicle, CreateVehicleError>.Failure(
                CreateVehicleError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Vehicle, UpdateVehicleError>> Handle(UpdateVehicleCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await vehicleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Vehicle {Id} was not found for update", command.Id);
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.VehicleNotFound);
        }

        if (await vehicleRepository.FindByCodeAsync(command.Code, cancellationToken) is { } byCode && byCode.Id != entity.Id)
        {
            logger.LogWarning(
                "Duplicate unit code rejected while updating vehicle {Id}",
                command.Id);
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.DuplicateVehicleCode);
        }

        if (await vehicleRepository.FindByPlateAsync(command.Plate, cancellationToken) is { } byPlate && byPlate.Id != entity.Id)
        {
            logger.LogWarning(
                "Duplicate license plate rejected while updating vehicle {Id}",
                command.Id);
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.DuplicateLicensePlate);
        }

        try
        {
            entity.Update(command);
            vehicleRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Vehicle, UpdateVehicleError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating vehicle");
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating vehicle");
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.DuplicateVehicleCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating vehicle");
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating vehicle");
            return new Result<Vehicle, UpdateVehicleError>.Failure(
                UpdateVehicleError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Vehicle, DeleteVehicleError>> Handle(DeleteVehicleCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await vehicleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Vehicle {Id} was not found for deletion", command.Id);
            return new Result<Vehicle, DeleteVehicleError>.Failure(
                DeleteVehicleError.VehicleNotFound);
        }

        if (await assignmentRepository.FindActiveByVehicleIdAsync(entity.Id, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Vehicle {Id} cannot be deleted because it has an active assignment",
                entity.Id);
            return new Result<Vehicle, DeleteVehicleError>.Failure(
                DeleteVehicleError.VehicleHasActiveAssignment);
        }

        try
        {
            vehicleRepository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Vehicle, DeleteVehicleError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Vehicle {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<Vehicle, DeleteVehicleError>.Failure(
                DeleteVehicleError.VehicleHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting vehicle");
            return new Result<Vehicle, DeleteVehicleError>.Failure(
                DeleteVehicleError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting vehicle");
            return new Result<Vehicle, DeleteVehicleError>.Failure(
                DeleteVehicleError.UnexpectedError);
        }
    }
}
