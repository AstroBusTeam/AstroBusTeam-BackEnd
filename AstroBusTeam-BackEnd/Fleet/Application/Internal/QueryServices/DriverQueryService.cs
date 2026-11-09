using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying drivers.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving drivers. All queries delegate to the repository layer.
/// </remarks>
/// <param name="driverRepository">Repository for accessing driver data.</param>
public class DriverQueryService(IDriverRepository driverRepository) : IDriverQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Driver>> Handle(GetAllDriversQuery query, CancellationToken cancellationToken = default) =>
        await driverRepository.FindAllAsync(query.EmployeeCode, cancellationToken);

    /// <inheritdoc />
    public async Task<Driver?> Handle(GetDriverByIdQuery query, CancellationToken cancellationToken = default) =>
        await driverRepository.FindByIdAsync(query.Id, cancellationToken);
}
