using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework repository for driver persistence.
/// </summary>
/// <remarks>
///     This repository extends the generic BaseRepository with driver-specific query methods.
/// </remarks>
/// <param name="context">The EF Core database context.</param>
public class DriverRepository(AppDbContext context)
    : BaseRepository<Driver>(context), IDriverRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Driver>> FindAllAsync(EmployeeCode? employeeCode = null, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Driver>().AsQueryable();
        if (employeeCode is not null) query = query.Where(d => d.EmployeeCode == employeeCode);
        return await query.OrderBy(d => d.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Driver?> FindByEmployeeCodeAsync(EmployeeCode employeeCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Driver>().FirstOrDefaultAsync(d => d.EmployeeCode == employeeCode, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<Driver?> FindByDniAsync(Dni dni, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Driver>().FirstOrDefaultAsync(d => d.Dni == dni, cancellationToken);
    }
}
