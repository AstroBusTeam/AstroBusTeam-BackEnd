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
///     Application service for handling delivery commands.
/// </summary>
/// <remarks>
///     This service acts as the command handler for the delivery aggregate. It enforces the business rules that
///     need data from other aggregates, then coordinates with the unit of work to persist changes.
///     Expected failures are returned as <see cref="Result{TValue, TError}"/> failures.
/// </remarks>
/// <param name="deliveryRepository">Repository for accessing delivery data.</param>
/// <param name="unitOfWork">Unit of work for managing transaction scope.</param>
/// <param name="logger">Logger for diagnostic and error reporting.</param>
public class DeliveryCommandService(
    IDeliveryRepository deliveryRepository,
    IUnitOfWork unitOfWork,
    ILogger<DeliveryCommandService> logger)
    : IDeliveryCommandService
{
    /// <inheritdoc />
    public async Task<Result<Delivery, CreateDeliveryError>> Handle(CreateDeliveryCommand command,
        CancellationToken cancellationToken = default)
    {

        try
        {
            var entity = new Delivery(command);
            await deliveryRepository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Delivery, CreateDeliveryError>.Success(entity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid data while creating delivery");
            return new Result<Delivery, CreateDeliveryError>.Failure(
                CreateDeliveryError.InvalidData);
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            logger.LogWarning(ex, "Invalid reference while creating delivery");
            return new Result<Delivery, CreateDeliveryError>.Failure(
                CreateDeliveryError.InvalidReference);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed while creating delivery");
            return new Result<Delivery, CreateDeliveryError>.Failure(
                CreateDeliveryError.UnexpectedError);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating delivery");
            return new Result<Delivery, CreateDeliveryError>.Failure(
                CreateDeliveryError.UnexpectedError);
        }
    }
}
