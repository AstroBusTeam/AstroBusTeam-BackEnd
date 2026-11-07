using SecurityBus.API.Fleet.Interfaces.ACL;
using SecurityBus.API.Operations.Application.Errors;
using SecurityBus.API.Operations.Application.Services;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.Commands;
using SecurityBus.API.Operations.Domain.Repositories;
using SecurityBus.API.Shared.Application.Extensions;
using SecurityBus.API.Shared.Application.Patterns;
using SecurityBus.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Operations.Application.Internal.CommandServices;

/// <summary>
///     Application service for handling shift commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the shift aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="shiftRepository">Repository for accessing shift data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class ShiftCommandService(
    IShiftRepository shiftRepository,
    IFleetContextFacade fleetContextFacade,
    IUnitOfWork unitOfWork,
    ILogger<ShiftCommandService> logger)
    : IShiftCommandService
{
    /// <inheritdoc />
    public async Task<Result<Shift, CreateShiftError>> Handle(CreateShiftCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await fleetContextFacade.FindDriverByIdAsync(command.DriverId, cancellationToken) is null)
        {
            logger.LogWarning(
                "Shift rejected because driver {DriverId} does not exist",
                command.DriverId);
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.DriverNotFound);
        }

        if (await fleetContextFacade.FindVehicleByIdAsync(command.VehicleId, cancellationToken) is null)
        {
            logger.LogWarning(
                "Shift rejected because vehicle {VehicleId} does not exist",
                command.VehicleId);
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.VehicleNotFound);
        }

        if (command.Status.IsActive && await shiftRepository.FindActiveByDriverIdAsync(command.DriverId, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Shift rejected because driver {DriverId} already has an active shift",
                command.DriverId);
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.ActiveShiftAlreadyExists);
        }

        try
        {
            var entity = new Shift(command);
            await shiftRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Shift, CreateShiftError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating shift");
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.InvalidData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating shift");
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating shift");
            return new Result<Shift, CreateShiftError>.Failure(
                CreateShiftError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Shift, UpdateShiftError>> Handle(UpdateShiftCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await shiftRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Shift {Id} was not found for update", command.Id);
            return new Result<Shift, UpdateShiftError>.Failure(
                UpdateShiftError.ShiftNotFound);
        }

        if (!entity.Status.IsActive && command.Status.IsActive)
        {
            logger.LogWarning(
                "Shift {Id} cannot be reopened once it has ended",
                entity.Id);
            return new Result<Shift, UpdateShiftError>.Failure(
                UpdateShiftError.InvalidShiftStateTransition);
        }

        try
        {
            entity.Update(command);
            shiftRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Shift, UpdateShiftError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating shift");
            return new Result<Shift, UpdateShiftError>.Failure(
                UpdateShiftError.InvalidData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating shift");
            return new Result<Shift, UpdateShiftError>.Failure(
                UpdateShiftError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating shift");
            return new Result<Shift, UpdateShiftError>.Failure(
                UpdateShiftError.UnexpectedError);
        }
    }
}
