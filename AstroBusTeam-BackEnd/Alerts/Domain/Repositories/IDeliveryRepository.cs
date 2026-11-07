using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

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
