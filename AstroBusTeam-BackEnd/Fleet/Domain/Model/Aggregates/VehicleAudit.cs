using AstroBusTeamBackEnd.Shared.Domain.Model;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Creation timestamp of the vehicle, managed by the persistence layer
/// </summary>
public partial class Vehicle : ICreatedAtEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }
}
