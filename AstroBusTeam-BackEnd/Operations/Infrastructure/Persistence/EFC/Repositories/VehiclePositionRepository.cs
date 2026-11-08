using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Operations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core repository for vehicle positions
/// </summary>
public class VehiclePositionRepository(AppDbContext context)
    : BaseRepository<VehiclePosition>(context), IVehiclePositionRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<VehiclePosition>> FindAllAsync(int? vehicleId = null, int? shiftId = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<VehiclePosition>().AsQueryable();
        if (vehicleId is not null) query = query.Where(e => e.VehicleId == vehicleId.Value);
        if (shiftId is not null) query = query.Where(e => e.ShiftId == shiftId.Value);
        return await query.OrderBy(e => e.Id).ToListAsync(cancellationToken);
    }
}
