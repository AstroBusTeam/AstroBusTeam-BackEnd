using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying deliveries.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving deliveries. All queries delegate to the repository layer.
/// </remarks>
/// <param name="deliveryRepository">Repository for accessing delivery data.</param>
public class DeliveryQueryService(IDeliveryRepository deliveryRepository) : IDeliveryQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Delivery>> Handle(GetAllDeliveriesQuery query, CancellationToken cancellationToken = default) =>
        await deliveryRepository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken = default) =>
        await deliveryRepository.FindByIdAsync(query.Id, cancellationToken);
}
