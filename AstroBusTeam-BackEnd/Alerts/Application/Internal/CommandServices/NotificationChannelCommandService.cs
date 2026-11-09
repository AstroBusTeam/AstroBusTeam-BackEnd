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
///     Command service implementation for notification channels
/// </summary>
public class NotificationChannelCommandService(
    INotificationChannelRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<NotificationChannelCommandService> logger)
    : INotificationChannelCommandService
{
    /// <inheritdoc />
    public async Task<Result<NotificationChannel, CreateNotificationChannelError>> Handle(CreateNotificationChannelCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is not null)
        {
            logger.LogWarning("Duplicate Code rejected for notification channel");
            return new Result<NotificationChannel, CreateNotificationChannelError>.Failure(
                CreateNotificationChannelError.DuplicateCode);
        }

        try
        {
            var entity = new NotificationChannel(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<NotificationChannel, CreateNotificationChannelError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating notification channel");
            return new Result<NotificationChannel, CreateNotificationChannelError>.Failure(
                CreateNotificationChannelError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating notification channel");
            return new Result<NotificationChannel, CreateNotificationChannelError>.Failure(
                CreateNotificationChannelError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating notification channel");
            return new Result<NotificationChannel, CreateNotificationChannelError>.Failure(
                CreateNotificationChannelError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating notification channel");
            return new Result<NotificationChannel, CreateNotificationChannelError>.Failure(
                CreateNotificationChannelError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<NotificationChannel, UpdateNotificationChannelError>> Handle(UpdateNotificationChannelCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("NotificationChannel {Id} was not found for update", command.Id);
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.NotificationChannelNotFound);
        }

        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is { } duplicate && duplicate.Id != entity.Id)
        {
            logger.LogWarning("Duplicate Code rejected while updating notification channel {Id}", command.Id);
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.DuplicateCode);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating notification channel");
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating notification channel");
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating notification channel");
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating notification channel");
            return new Result<NotificationChannel, UpdateNotificationChannelError>.Failure(
                UpdateNotificationChannelError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<NotificationChannel, DeleteNotificationChannelError>> Handle(DeleteNotificationChannelCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("NotificationChannel {Id} was not found for deletion", command.Id);
            return new Result<NotificationChannel, DeleteNotificationChannelError>.Failure(
                DeleteNotificationChannelError.NotificationChannelNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<NotificationChannel, DeleteNotificationChannelError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "NotificationChannel {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<NotificationChannel, DeleteNotificationChannelError>.Failure(
                DeleteNotificationChannelError.NotificationChannelHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting notification channel");
            return new Result<NotificationChannel, DeleteNotificationChannelError>.Failure(
                DeleteNotificationChannelError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting notification channel");
            return new Result<NotificationChannel, DeleteNotificationChannelError>.Failure(
                DeleteNotificationChannelError.UnexpectedError);
        }
    }
}
