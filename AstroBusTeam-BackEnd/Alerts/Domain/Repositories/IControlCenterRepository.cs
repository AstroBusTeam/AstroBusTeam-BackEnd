using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Domain.Repositories;

/// <summary>
///     Repository for control centers
/// </summary>
public interface IControlCenterRepository : IBaseRepository<ControlCenter>
{
    /// <summary>
    ///     Finds control centers, optionally filtered.
    /// </summary>
    Task<IEnumerable<ControlCenter>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<ControlCenter?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
}
