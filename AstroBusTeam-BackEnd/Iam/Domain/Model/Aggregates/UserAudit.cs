using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;

/// <summary>
///     Creation timestamp of the user, managed by the persistence layer
/// </summary>
public partial class User : ICreatedAtEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }
}
