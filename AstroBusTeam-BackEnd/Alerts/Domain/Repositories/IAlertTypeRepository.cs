using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     Repository for alert types
/// </summary>
public interface IAlertTypeRepository : IBaseRepository<AlertType>
{
    /// <summary>
    ///     Finds alert types, optionally filtered.
    /// </summary>
    Task<IEnumerable<AlertType>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<AlertType?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
