using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Creation timestamp of the driver, managed by the persistence layer
/// </summary>
public partial class Driver : ICreatedAtEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }
}
