using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Fleet.Domain.Model.ValueObjects;
using SecurityBus.API.Fleet.Domain.Repositories;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SecurityBus.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SecurityBus.API.Fleet.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for vehicle persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with vehicle-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class VehicleRepository(AppDbContext context)
    : BaseRepository<Vehicle>(context), IVehicleRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Vehicle>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Vehicle>().AsQueryable();

        return await query.OrderBy(v => v.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Vehicle?> FindByCodeAsync(VehicleCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Vehicle>().FirstOrDefaultAsync(v => v.Code == code, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<Vehicle?> FindByPlateAsync(LicensePlate plate, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Vehicle>().FirstOrDefaultAsync(v => v.Plate == plate, cancellationToken);
    }
}
