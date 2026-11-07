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
///     Application service for handling driver commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the driver aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="driverRepository">Repository for accessing driver data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class DriverCommandService(
    IDriverRepository driverRepository,
    IAssignmentRepository assignmentRepository,
    IUnitOfWork unitOfWork,
    ILogger<DriverCommandService> logger)
    : IDriverCommandService
{
    /// <inheritdoc />
    public async Task<Result<Driver, CreateDriverError>> Handle(CreateDriverCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await driverRepository.FindByEmployeeCodeAsync(command.EmployeeCode, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Duplicate employee code rejected for driver {EmployeeCode}",
                command.EmployeeCode);
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.DuplicateEmployeeCode);
        }

        if (await driverRepository.FindByDniAsync(command.Dni, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Duplicate DNI rejected for driver with DNI {Dni}",
                command.Dni.MaskedForLogging());
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.DuplicateDni);
        }

        try
        {
            var entity = new Driver(command);
            await driverRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Driver, CreateDriverError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating driver");
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating driver");
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.DuplicateEmployeeCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating driver");
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating driver");
            return new Result<Driver, CreateDriverError>.Failure(
                CreateDriverError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Driver, UpdateDriverError>> Handle(UpdateDriverCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await driverRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Driver {Id} was not found for update", command.Id);
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.DriverNotFound);
        }

        if (await driverRepository.FindByEmployeeCodeAsync(command.EmployeeCode, cancellationToken) is { } byCode && byCode.Id != entity.Id)
        {
            logger.LogWarning(
                "Duplicate employee code rejected while updating driver {Id}",
                command.Id);
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.DuplicateEmployeeCode);
        }

        if (await driverRepository.FindByDniAsync(command.Dni, cancellationToken) is { } byDni && byDni.Id != entity.Id)
        {
            logger.LogWarning(
                "Duplicate DNI rejected while updating driver {Id}",
                command.Id);
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.DuplicateDni);
        }

        try
        {
            entity.Update(command);
            driverRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Driver, UpdateDriverError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating driver");
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating driver");
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.DuplicateEmployeeCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating driver");
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating driver");
            return new Result<Driver, UpdateDriverError>.Failure(
                UpdateDriverError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Driver, DeleteDriverError>> Handle(DeleteDriverCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await driverRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Driver {Id} was not found for deletion", command.Id);
            return new Result<Driver, DeleteDriverError>.Failure(
                DeleteDriverError.DriverNotFound);
        }

        if (await assignmentRepository.FindActiveByDriverIdAsync(entity.Id, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Driver {Id} cannot be deleted because it has an active assignment",
                entity.Id);
            return new Result<Driver, DeleteDriverError>.Failure(
                DeleteDriverError.DriverHasActiveAssignment);
        }

        try
        {
            driverRepository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Driver, DeleteDriverError>.Success(entity);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting driver");
            return new Result<Driver, DeleteDriverError>.Failure(
                DeleteDriverError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting driver");
            return new Result<Driver, DeleteDriverError>.Failure(
                DeleteDriverError.UnexpectedError);
        }
    }
}
