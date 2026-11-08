namespace AstroBusTeamBackEnd.Iam.Domain.Model.Commands;

/// <summary>
///     Command to create a user
/// </summary>
public record CreateUserCommand(string Username, string PasswordHash, string Role, int? DriverId, bool Active);
