using AstroBusTeamBackEnd.Operations.Application.Errors;
using AstroBusTeamBackEnd.Operations.Application.Services;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Application.Extensions;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Operations.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for vehicle positions
/// </summary>
public class VehiclePositionCommandService(
    IVehiclePositionRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<VehiclePositionCommandService> logger)
    : IVehiclePositionCommandService
{
    /// <inheritdoc />
    public async Task<Result<VehiclePosition, CreateVehiclePositionError>> Handle(CreateVehiclePositionCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new VehiclePosition(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<VehiclePosition, CreateVehiclePositionError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating vehicle position");
            return new Result<VehiclePosition, CreateVehiclePositionError>.Failure(
                CreateVehiclePositionError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating vehicle position");
            return new Result<VehiclePosition, CreateVehiclePositionError>.Failure(
                CreateVehiclePositionError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating vehicle position");
            return new Result<VehiclePosition, CreateVehiclePositionError>.Failure(
                CreateVehiclePositionError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating vehicle position");
            return new Result<VehiclePosition, CreateVehiclePositionError>.Failure(
                CreateVehiclePositionError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<VehiclePosition, UpdateVehiclePositionError>> Handle(UpdateVehiclePositionCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("VehiclePosition {Id} was not found for update", command.Id);
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Failure(
                UpdateVehiclePositionError.VehiclePositionNotFound);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating vehicle position");
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Failure(
                UpdateVehiclePositionError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while updating vehicle position");
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Failure(
                UpdateVehiclePositionError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating vehicle position");
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Failure(
                UpdateVehiclePositionError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating vehicle position");
            return new Result<VehiclePosition, UpdateVehiclePositionError>.Failure(
                UpdateVehiclePositionError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<VehiclePosition, DeleteVehiclePositionError>> Handle(DeleteVehiclePositionCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("VehiclePosition {Id} was not found for deletion", command.Id);
            return new Result<VehiclePosition, DeleteVehiclePositionError>.Failure(
                DeleteVehiclePositionError.VehiclePositionNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<VehiclePosition, DeleteVehiclePositionError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "VehiclePosition {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<VehiclePosition, DeleteVehiclePositionError>.Failure(
                DeleteVehiclePositionError.VehiclePositionHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting vehicle position");
            return new Result<VehiclePosition, DeleteVehiclePositionError>.Failure(
                DeleteVehiclePositionError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting vehicle position");
            return new Result<VehiclePosition, DeleteVehiclePositionError>.Failure(
                DeleteVehiclePositionError.UnexpectedError);
        }
    }
}
