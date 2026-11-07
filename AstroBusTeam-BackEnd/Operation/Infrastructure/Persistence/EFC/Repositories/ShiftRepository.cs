using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Operations.Domain.Model.ValueObjects;
using SecurityBus.API.Operations.Domain.Repositories;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Operations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for shift persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with shift-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class ShiftRepository(AppDbContext context)
    : BaseRepository<Shift>(context), IShiftRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Shift>> FindAllAsync(int? driverId = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Shift>().AsQueryable();
        if (driverId is not null) query = query.Where(s => s.DriverId == driverId.Value);
        return await query.OrderBy(s => s.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Shift?> FindActiveByDriverIdAsync(int driverId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Shift>().FirstOrDefaultAsync(s => s.DriverId == driverId && s.Status == ShiftStatus.Active, cancellationToken);
    }
}
