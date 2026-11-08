namespace AstroBusTeamBackEnd.Iam.Domain.Model.Commands;

/// <summary>
///     Command to update a user
/// </summary>
public record UpdateUserCommand(int Id, string Username, string? PasswordHash, string Role, int? DriverId, bool Active);
