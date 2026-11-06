using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Queries;

namespace SecurityBus.API.Alerts.Application.Services;

/// <summary>
///     Query service interface for delivery operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying deliveries. It supports listing (with optional filters) and
///     retrieving a single delivery by its identifier.
/// </remarks>
public interface IDeliveryQueryService
{
    /// <summary>
    ///     Handle the GetAllDeliveriesQuery.
    /// </summary>
    /// <param name="query">The GetAllDeliveriesQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Delivery objects</returns>
    Task<IEnumerable<Delivery>> Handle(GetAllDeliveriesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetDeliveryByIdQuery.
    /// </summary>
    /// <param name="query">The GetDeliveryByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Delivery object if found, or null otherwise</returns>
    Task<Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken = default);
}
