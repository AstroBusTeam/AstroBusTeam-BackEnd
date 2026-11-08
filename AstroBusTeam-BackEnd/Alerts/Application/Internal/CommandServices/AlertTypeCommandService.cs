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
///     Command service implementation for alert types
/// </summary>
public class AlertTypeCommandService(
    IAlertTypeRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<AlertTypeCommandService> logger)
    : IAlertTypeCommandService
{
    /// <inheritdoc />
    public async Task<Result<AlertType, CreateAlertTypeError>> Handle(CreateAlertTypeCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await repository.FindByNameAsync(command.Name, cancellationToken) is not null)
        {
            logger.LogWarning("Duplicate Name rejected for alert type");
            return new Result<AlertType, CreateAlertTypeError>.Failure(
                CreateAlertTypeError.DuplicateName);
        }

        try
        {
            var entity = new AlertType(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertType, CreateAlertTypeError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating alert type");
            return new Result<AlertType, CreateAlertTypeError>.Failure(
                CreateAlertTypeError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating alert type");
            return new Result<AlertType, CreateAlertTypeError>.Failure(
                CreateAlertTypeError.DuplicateName);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating alert type");
            return new Result<AlertType, CreateAlertTypeError>.Failure(
                CreateAlertTypeError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert type");
            return new Result<AlertType, CreateAlertTypeError>.Failure(
                CreateAlertTypeError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<AlertType, UpdateAlertTypeError>> Handle(UpdateAlertTypeCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("AlertType {Id} was not found for update", command.Id);
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.AlertTypeNotFound);
        }

        if (await repository.FindByNameAsync(command.Name, cancellationToken) is { } duplicate && duplicate.Id != entity.Id)
        {
            logger.LogWarning("Duplicate Name rejected while updating alert type {Id}", command.Id);
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.DuplicateName);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertType, UpdateAlertTypeError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating alert type");
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating alert type");
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.DuplicateName);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating alert type");
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert type");
            return new Result<AlertType, UpdateAlertTypeError>.Failure(
                UpdateAlertTypeError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<AlertType, DeleteAlertTypeError>> Handle(DeleteAlertTypeCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("AlertType {Id} was not found for deletion", command.Id);
            return new Result<AlertType, DeleteAlertTypeError>.Failure(
                DeleteAlertTypeError.AlertTypeNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertType, DeleteAlertTypeError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "AlertType {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<AlertType, DeleteAlertTypeError>.Failure(
                DeleteAlertTypeError.AlertTypeHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting alert type");
            return new Result<AlertType, DeleteAlertTypeError>.Failure(
                DeleteAlertTypeError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert type");
            return new Result<AlertType, DeleteAlertTypeError>.Failure(
                DeleteAlertTypeError.UnexpectedError);
        }
    }
}
