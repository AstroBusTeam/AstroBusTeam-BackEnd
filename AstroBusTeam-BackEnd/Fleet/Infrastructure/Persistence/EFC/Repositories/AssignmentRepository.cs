using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for assignment persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with assignment-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class AssignmentRepository(AppDbContext context)
    : BaseRepository<Assignment>(context), IAssignmentRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Assignment>> FindAllAsync(int? driverId = null, AssignmentStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Assignment>().AsQueryable();
        if (driverId is not null) query = query.Where(a => a.DriverId == driverId.Value);
        if (status is not null) query = query.Where(a => a.Status == status);
        return await query.OrderBy(a => a.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Assignment?> FindActiveByDriverIdAsync(int driverId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Assignment>().FirstOrDefaultAsync(a => a.DriverId == driverId && a.Status == AssignmentStatus.Active, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<Assignment?> FindActiveByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Assignment>().FirstOrDefaultAsync(a => a.VehicleId == vehicleId && a.Status == AssignmentStatus.Active, cancellationToken);
    }
}
