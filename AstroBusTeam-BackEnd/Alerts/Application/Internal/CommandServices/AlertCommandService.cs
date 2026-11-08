using AstroBusTeamBackEnd.Fleet.Interfaces.ACL;
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
///     Application service for handling alert commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the alert aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="alertRepository">Repository for accessing alert data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class AlertCommandService(
    IAlertRepository alertRepository,
    IFleetContextFacade fleetContextFacade,
    IUnitOfWork unitOfWork,
    ILogger<AlertCommandService> logger)
    : IAlertCommandService
{
    /// <inheritdoc />
    public async Task<Result<Alert, CreateAlertError>> Handle(CreateAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await fleetContextFacade.FindVehicleByIdAsync(command.VehicleId, cancellationToken) is null)
        {
            logger.LogWarning(
                "Alert rejected because vehicle {VehicleId} does not exist",
                command.VehicleId);
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.VehicleNotFound);
        }

        if (await alertRepository.FindByCodeAsync(command.Code, cancellationToken) is not null)
        {
            logger.LogWarning(
                "Duplicate alert code rejected for {Code}",
                command.Code);
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.DuplicateAlertCode);
        }

        try
        {
            var entity = new Alert(command);
            await alertRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Alert, CreateAlertError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating alert");
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating alert");
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.DuplicateAlertCode);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating alert");
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating alert");
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert");
            return new Result<Alert, CreateAlertError>.Failure(
                CreateAlertError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Alert, UpdateAlertError>> Handle(UpdateAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await alertRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Alert {Id} was not found for update", command.Id);
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.AlertNotFound);
        }

        if (await alertRepository.FindByCodeAsync(command.Code, cancellationToken) is { } byCode && byCode.Id != entity.Id)
        {
            logger.LogWarning(
                "Duplicate alert code rejected while updating alert {Id}",
                command.Id);
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.DuplicateAlertCode);
        }

        if (command.Attempts < entity.Attempts)
        {
            logger.LogWarning(
                "Alert {Id} attempts cannot decrease",
                entity.Id);
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.AttemptsCannotDecrease);
        }

        try
        {
            entity.Update(command);
            alertRepository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Alert, UpdateAlertError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating alert");
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating alert");
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.DuplicateAlertCode);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while updating alert");
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating alert");
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert");
            return new Result<Alert, UpdateAlertError>.Failure(
                UpdateAlertError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Alert, DeleteAlertError>> Handle(DeleteAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await alertRepository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("Alert {Id} was not found for deletion", command.Id);
            return new Result<Alert, DeleteAlertError>.Failure(
                DeleteAlertError.AlertNotFound);
        }


        try
        {
            alertRepository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Alert, DeleteAlertError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Alert {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<Alert, DeleteAlertError>.Failure(
                DeleteAlertError.AlertHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting alert");
            return new Result<Alert, DeleteAlertError>.Failure(
                DeleteAlertError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert");
            return new Result<Alert, DeleteAlertError>.Failure(
                DeleteAlertError.UnexpectedError);
        }
    }
}
