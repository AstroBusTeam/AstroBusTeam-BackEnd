using SecurityBus.API.Operations.Application.Services;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.Queries;
using SecurityBus.API.Operations.Domain.Repositories;

namespace SecurityBus.API.Operations.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying shifts.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving shifts. All queries delegate to the repository layer.
/// </remarks>
/// <param name="shiftRepository">Repository for accessing shift data.</param>
public class ShiftQueryService(IShiftRepository shiftRepository) : IShiftQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Shift>> Handle(GetAllShiftsQuery query, CancellationToken cancellationToken = default) =>
        await shiftRepository.FindAllAsync(query.DriverId, cancellationToken);

    /// <inheritdoc />
    public async Task<Shift?> Handle(GetShiftByIdQuery query, CancellationToken cancellationToken = default) =>
        await shiftRepository.FindByIdAsync(query.Id, cancellationToken);
}
