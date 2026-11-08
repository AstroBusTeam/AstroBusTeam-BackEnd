using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Application.Extensions;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for control centers
/// </summary>
public class ControlCenterCommandService(
    IControlCenterRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<ControlCenterCommandService> logger)
    : IControlCenterCommandService
{
    /// <inheritdoc />
    public async Task<Result<ControlCenter, CreateControlCenterError>> Handle(CreateControlCenterCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is not null)
        {
            logger.LogWarning("Duplicate Code rejected for control center");
            return new Result<ControlCenter, CreateControlCenterError>.Failure(
                CreateControlCenterError.DuplicateCode);
        }

        try
        {
            var entity = new ControlCenter(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<ControlCenter, CreateControlCenterError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating control center");
            return new Result<ControlCenter, CreateControlCenterError>.Failure(
                CreateControlCenterError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating control center");
            return new Result<ControlCenter, CreateControlCenterError>.Failure(
                CreateControlCenterError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating control center");
            return new Result<ControlCenter, CreateControlCenterError>.Failure(
                CreateControlCenterError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating control center");
            return new Result<ControlCenter, CreateControlCenterError>.Failure(
                CreateControlCenterError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<ControlCenter, UpdateControlCenterError>> Handle(UpdateControlCenterCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("ControlCenter {Id} was not found for update", command.Id);
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.ControlCenterNotFound);
        }

        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is { } duplicate && duplicate.Id != entity.Id)
        {
            logger.LogWarning("Duplicate Code rejected while updating control center {Id}", command.Id);
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.DuplicateCode);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<ControlCenter, UpdateControlCenterError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating control center");
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating control center");
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating control center");
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating control center");
            return new Result<ControlCenter, UpdateControlCenterError>.Failure(
                UpdateControlCenterError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<ControlCenter, DeleteControlCenterError>> Handle(DeleteControlCenterCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("ControlCenter {Id} was not found for deletion", command.Id);
            return new Result<ControlCenter, DeleteControlCenterError>.Failure(
                DeleteControlCenterError.ControlCenterNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<ControlCenter, DeleteControlCenterError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "ControlCenter {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<ControlCenter, DeleteControlCenterError>.Failure(
                DeleteControlCenterError.ControlCenterHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting control center");
            return new Result<ControlCenter, DeleteControlCenterError>.Failure(
                DeleteControlCenterError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting control center");
            return new Result<ControlCenter, DeleteControlCenterError>.Failure(
                DeleteControlCenterError.UnexpectedError);
        }
    }
}
