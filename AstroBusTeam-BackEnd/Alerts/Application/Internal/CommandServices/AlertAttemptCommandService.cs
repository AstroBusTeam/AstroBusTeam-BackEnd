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
///     Command service implementation for alert attempts
/// </summary>
public class AlertAttemptCommandService(
    IAlertAttemptRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<AlertAttemptCommandService> logger)
    : IAlertAttemptCommandService
{
    /// <inheritdoc />
    public async Task<Result<AlertAttempt, CreateAlertAttemptError>> Handle(CreateAlertAttemptCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new AlertAttempt(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertAttempt, CreateAlertAttemptError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating alert attempt");
            return new Result<AlertAttempt, CreateAlertAttemptError>.Failure(
                CreateAlertAttemptError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating alert attempt");
            return new Result<AlertAttempt, CreateAlertAttemptError>.Failure(
                CreateAlertAttemptError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating alert attempt");
            return new Result<AlertAttempt, CreateAlertAttemptError>.Failure(
                CreateAlertAttemptError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert attempt");
            return new Result<AlertAttempt, CreateAlertAttemptError>.Failure(
                CreateAlertAttemptError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<AlertAttempt, UpdateAlertAttemptError>> Handle(UpdateAlertAttemptCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("AlertAttempt {Id} was not found for update", command.Id);
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Failure(
                UpdateAlertAttemptError.AlertAttemptNotFound);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating alert attempt");
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Failure(
                UpdateAlertAttemptError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while updating alert attempt");
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Failure(
                UpdateAlertAttemptError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating alert attempt");
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Failure(
                UpdateAlertAttemptError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert attempt");
            return new Result<AlertAttempt, UpdateAlertAttemptError>.Failure(
                UpdateAlertAttemptError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<AlertAttempt, DeleteAlertAttemptError>> Handle(DeleteAlertAttemptCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("AlertAttempt {Id} was not found for deletion", command.Id);
            return new Result<AlertAttempt, DeleteAlertAttemptError>.Failure(
                DeleteAlertAttemptError.AlertAttemptNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<AlertAttempt, DeleteAlertAttemptError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "AlertAttempt {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<AlertAttempt, DeleteAlertAttemptError>.Failure(
                DeleteAlertAttemptError.AlertAttemptHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting alert attempt");
            return new Result<AlertAttempt, DeleteAlertAttemptError>.Failure(
                DeleteAlertAttemptError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert attempt");
            return new Result<AlertAttempt, DeleteAlertAttemptError>.Failure(
                DeleteAlertAttemptError.UnexpectedError);
        }
    }
}
