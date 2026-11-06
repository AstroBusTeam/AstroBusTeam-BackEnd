using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Domain.Model;

namespace SecurityBus.API.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a recipient in the Alerts bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Recipient
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Recipient()
    {
        Name = string.Empty;
        Role = string.Empty;
        Channel = string.Empty;
    }

    /// <summary>
    ///     Creates a new Recipient from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateRecipientCommand.
    /// </remarks>
    /// <param name="command">The CreateRecipientCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Recipient(CreateRecipientCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        Role = command.Role;
        Channel = command.Channel;
        Active = command.Active;
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this recipient.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the recipient name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the role of the recipient.
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    ///     Gets the notification channel.
    /// </summary>
    public string Channel { get; private set; }

    /// <summary>
    ///     Gets the whether the recipient receives alerts.
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    ///     Updates the recipient with the values of an update command.
    /// </summary>
    /// <remarks>
    ///     This method acts as the command handler for UpdateRecipientCommand. The invariants are validated again.
    /// </remarks>
    /// <param name="command">The UpdateRecipientCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public void Update(UpdateRecipientCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        Role = command.Role;
        Channel = command.Channel;
        Active = command.Active;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Name, nameof(Name));
        Guard.NotLongerThan(Name, 100, nameof(Name));
        Guard.NotBlank(Role, nameof(Role));
        Guard.NotLongerThan(Role, 100, nameof(Role));
        Guard.NotBlank(Channel, nameof(Channel));
        Guard.NotLongerThan(Channel, 50, nameof(Channel));
    }
}
