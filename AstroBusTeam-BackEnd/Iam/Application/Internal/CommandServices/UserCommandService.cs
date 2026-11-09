using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Application.Services;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Application.Extensions;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Iam.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for users
/// </summary>
public class UserCommandService(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UserCommandService> logger)
    : IUserCommandService
{
    /// <inheritdoc />
    public async Task<Result<User, CreateUserError>> Handle(CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await repository.FindByUsernameAsync(command.Username, cancellationToken) is not null)
        {
            logger.LogWarning("Duplicate Username rejected for user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.DuplicateUsername);
        }

        try
        {
            var entity = new User(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<User, CreateUserError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.DuplicateUsername);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating user");
            return new Result<User, CreateUserError>.Failure(
                CreateUserError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<User, UpdateUserError>> Handle(UpdateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("User {Id} was not found for update", command.Id);
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.UserNotFound);
        }

        if (await repository.FindByUsernameAsync(command.Username, cancellationToken) is { } duplicate && duplicate.Id != entity.Id)
        {
            logger.LogWarning("Duplicate Username rejected while updating user {Id}", command.Id);
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.DuplicateUsername);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<User, UpdateUserError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating user");
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating user");
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.DuplicateUsername);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while updating user");
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating user");
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating user");
            return new Result<User, UpdateUserError>.Failure(
                UpdateUserError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<User, DeleteUserError>> Handle(DeleteUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("User {Id} was not found for deletion", command.Id);
            return new Result<User, DeleteUserError>.Failure(
                DeleteUserError.UserNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<User, DeleteUserError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "User {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<User, DeleteUserError>.Failure(
                DeleteUserError.UserHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting user");
            return new Result<User, DeleteUserError>.Failure(
                DeleteUserError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting user");
            return new Result<User, DeleteUserError>.Failure(
                DeleteUserError.UnexpectedError);
        }
    }
}
