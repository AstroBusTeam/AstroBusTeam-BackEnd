namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a user
/// </summary>
public record UserResource(
    int Id,
    string Username,
    string Role,
    int? DriverId,
    bool Active,
    DateTime? LastLoginAt,
    DateTimeOffset? CreatedAt);
