using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Application.Extensions;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for routes
/// </summary>
public class BusRouteCommandService(
    IBusRouteRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<BusRouteCommandService> logger)
    : IBusRouteCommandService
{
    /// <inheritdoc />
    public async Task<Result<BusRoute, CreateBusRouteError>> Handle(CreateBusRouteCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is not null)
        {
            logger.LogWarning("Duplicate Code rejected for route");
            return new Result<BusRoute, CreateBusRouteError>.Failure(
                CreateBusRouteError.DuplicateCode);
        }

        try
        {
            var entity = new BusRoute(command);
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<BusRoute, CreateBusRouteError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating route");
            return new Result<BusRoute, CreateBusRouteError>.Failure(
                CreateBusRouteError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while creating route");
            return new Result<BusRoute, CreateBusRouteError>.Failure(
                CreateBusRouteError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating route");
            return new Result<BusRoute, CreateBusRouteError>.Failure(
                CreateBusRouteError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating route");
            return new Result<BusRoute, CreateBusRouteError>.Failure(
                CreateBusRouteError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<BusRoute, UpdateBusRouteError>> Handle(UpdateBusRouteCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("BusRoute {Id} was not found for update", command.Id);
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.BusRouteNotFound);
        }

        if (await repository.FindByCodeAsync(command.Code, cancellationToken) is { } duplicate && duplicate.Id != entity.Id)
        {
            logger.LogWarning("Duplicate Code rejected while updating route {Id}", command.Id);
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.DuplicateCode);
        }

        try
        {
            entity.Update(command);
            repository.Update(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<BusRoute, UpdateBusRouteError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while updating route");
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
        {
            logger.LogWarning(ex, "Duplicate key violation while updating route");
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.DuplicateCode);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while updating route");
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating route");
            return new Result<BusRoute, UpdateBusRouteError>.Failure(
                UpdateBusRouteError.UnexpectedError);
        }
    }

    /// <inheritdoc />
    public async Task<Result<BusRoute, DeleteBusRouteError>> Handle(DeleteBusRouteCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            logger.LogWarning("BusRoute {Id} was not found for deletion", command.Id);
            return new Result<BusRoute, DeleteBusRouteError>.Failure(
                DeleteBusRouteError.BusRouteNotFound);
        }

        try
        {
            repository.Remove(entity);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<BusRoute, DeleteBusRouteError>.Success(entity);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "BusRoute {Id} cannot be deleted because it is referenced by other records", entity.Id);
            return new Result<BusRoute, DeleteBusRouteError>.Failure(
                DeleteBusRouteError.BusRouteHasRelatedData);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while deleting route");
            return new Result<BusRoute, DeleteBusRouteError>.Failure(
                DeleteBusRouteError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting route");
            return new Result<BusRoute, DeleteBusRouteError>.Failure(
                DeleteBusRouteError.UnexpectedError);
        }
    }
}
