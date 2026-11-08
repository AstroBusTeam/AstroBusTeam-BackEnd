using AstroBusTeamBackEnd.Operations.Application.Services;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Queries;
using AstroBusTeamBackEnd.Operations.Domain.Repositories;

namespace AstroBusTeamBackEnd.Operations.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for vehicle positions
/// </summary>
public class VehiclePositionQueryService(IVehiclePositionRepository repository) : IVehiclePositionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<VehiclePosition>> Handle(GetAllVehiclePositionsQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindAllAsync(query.VehicleId, query.ShiftId, cancellationToken);

    /// <inheritdoc />
    public async Task<VehiclePosition?> Handle(GetVehiclePositionByIdQuery query, CancellationToken cancellationToken = default) =>
        await repository.FindByIdAsync(query.Id, cancellationToken);
}
