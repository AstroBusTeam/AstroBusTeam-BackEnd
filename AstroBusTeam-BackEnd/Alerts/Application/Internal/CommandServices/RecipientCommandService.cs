using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Application.Services;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Domain.Repositories;
using SecurityBus.API.Shared.Application.Extensions;
using SecurityBus.API.Shared.Application.Patterns;
using SecurityBus.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Alerts.Application.Internal.CommandServices;

/// <summary>
///     Application service for handling recipient commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the recipient aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="recipientRepository">Repository for accessing recipient data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class RecipientCommandService(
    IRecipientRepository recipientRepository,
    IUnitOfWork unitOfWork,
    ILogger<RecipientCommandService> logger)
    : IRecipientCommandService
{
    /// <inheritdoc />
    public async Task<Result<Recipient, UpdateRecipientError>> Handle(UpdateRecipientCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await recipientRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Recipient {Id} was not found for update", command.Id);
            return new Result<Recipient, UpdateRecipientError>.Failure(
                UpdateRecipientError.RecipientNotFound);
        }


        try
        {
            entity.Update(command);
            recipientRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Recipient, UpdateRecipientError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating recipient");
            return new Result<Recipient, UpdateRecipientError>.Failure(
                UpdateRecipientError.InvalidData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating recipient");
            return new Result<Recipient, UpdateRecipientError>.Failure(
                UpdateRecipientError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating recipient");
            return new Result<Recipient, UpdateRecipientError>.Failure(
                UpdateRecipientError.UnexpectedError);
        }
    }
}
