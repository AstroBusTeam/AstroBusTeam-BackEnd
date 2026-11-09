using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Repositories;

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
