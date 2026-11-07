using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;

namespace AstroBusTeamBackEnd.Fleet.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying vehicles.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving vehicles. All queries delegate to the repository layer.
/// </remarks>
/// <param name="vehicleRepository">Repository for accessing vehicle data.</param>
public class VehicleQueryService(IVehicleRepository vehicleRepository) : IVehicleQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query, CancellationToken cancellationToken = default) =>
        await vehicleRepository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query, CancellationToken cancellationToken = default) =>
        await vehicleRepository.FindByIdAsync(query.Id, cancellationToken);
}
