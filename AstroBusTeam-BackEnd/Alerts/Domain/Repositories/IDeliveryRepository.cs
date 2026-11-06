using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Repositories;

namespace SecurityBus.API.Alerts.Domain.Repositories;

/// <summary>
///     The Delivery repository contract.
/// </summary>
public interface IDeliveryRepository : IBaseRepository<Delivery>
{
    /// <summary>
    ///     Find all deliveries, optionally filtered, ordered by identifier
    /// </summary>
    Task<IEnumerable<Delivery>> FindAllAsync(CancellationToken cancellationToken = default);
}
