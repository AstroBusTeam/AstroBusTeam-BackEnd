namespace AstroBusTeamBackEnd.Shared.Domain.Model;

/// <summary>
///     Marks an entity as carrying only a creation timestamp managed by the persistence layer.
/// </summary>
/// <remarks>
///     Any entity that implements this interface has <see cref="CreatedAt"/> set once on first persistence by
///     <c>AuditableEntityInterceptor</c>, unless the value was already assigned.
/// </remarks>
public interface ICreatedAtEntity
{
    /// <summary>
    ///     Gets or sets the UTC timestamp when the entity was first persisted.
    /// </summary>
    DateTimeOffset? CreatedAt { get; set; }
}
