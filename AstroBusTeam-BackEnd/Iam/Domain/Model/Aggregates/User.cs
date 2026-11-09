using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;

/// <summary>
///     Represents a user of the SecurityBus platform
/// </summary>
public partial class User
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected User()
    {
        Username = string.Empty;
        PasswordHash = string.Empty;
        Role = string.Empty;
    }

    /// <summary>
    ///     Creates a user from a command.
    /// </summary>
    /// <param name="command">The command with the user data.</param>
    public User(CreateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Username = command.Username;
        PasswordHash = command.PasswordHash;
        Role = command.Role.Trim().ToLowerInvariant();
        DriverId = command.DriverId;
        Active = command.Active;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public string Username { get; private set; }

    public string PasswordHash { get; private set; }

    public string Role { get; private set; }

    public int? DriverId { get; private set; }

    public bool Active { get; private set; }

    public DateTime? LastLoginAt { get; private set; }

    /// <summary>
    ///     Updates the user from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Username = command.Username;
        if (command.PasswordHash is not null) PasswordHash = command.PasswordHash;
        Role = command.Role.Trim().ToLowerInvariant();
        DriverId = command.DriverId;
        Active = command.Active;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Username, nameof(Username));
        Guard.NotLongerThan(Username, 50, nameof(Username));
        Guard.NotBlank(PasswordHash, nameof(PasswordHash));
        Guard.NotLongerThan(PasswordHash, 255, nameof(PasswordHash));
        Guard.OneOf(Role, ["admin", "operator", "driver"], nameof(Role));
        if (DriverId is not null) Guard.AtLeast(DriverId.Value, 1, nameof(DriverId));
    }
}
