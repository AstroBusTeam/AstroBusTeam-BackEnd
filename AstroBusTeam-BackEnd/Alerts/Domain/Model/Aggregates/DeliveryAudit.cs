using SecurityBus.API.Shared.Domain.Model;

namespace SecurityBus.API.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Audit extension for Delivery aggregate.
/// </summary>
/// <remarks>
///     This partial class extends Delivery with audit trail properties.
///     CreatedAt and UpdatedAt are automatically managed by the persistence layer
///     via the AuditableEntityInterceptor in the Shared bounded context.
///     Implements the IAuditableEntity interface to provide these properties.
/// </remarks>
public partial class Delivery : IAuditableEntity
{
    /// <summary>
    ///     Gets the timestamp when this delivery was created.
    /// </summary>
    /// <remarks>
    ///     Automatically set once by the persistence layer on first insert. Maps to column: created_at.
    /// </remarks>
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    ///     Gets the timestamp when this delivery was last updated.
    /// </summary>
    /// <remarks>
    ///     Automatically refreshed by the persistence layer on every save. Maps to column: updated_at.
    /// </remarks>
    public DateTimeOffset? UpdatedAt { get; set; }
}
