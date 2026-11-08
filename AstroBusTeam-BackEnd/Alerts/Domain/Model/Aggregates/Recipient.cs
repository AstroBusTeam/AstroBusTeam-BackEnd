using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a person or unit that receives alerts
/// </summary>
/// <remarks>
///     A recipient belongs to a control center and accepts alerts through one or more notification channels.
/// </remarks>
public partial class Recipient
{
    private readonly List<RecipientChannel> _channels = [];

    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Recipient()
    {
        Name = string.Empty;
        Role = string.Empty;
    }

    /// <summary>
    ///     Creates a recipient from a command.
    /// </summary>
    /// <param name="command">The command with the recipient data.</param>
    public Recipient(CreateRecipientCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        Role = command.Role;
        Email = command.Email;
        Phone = command.Phone;
        ControlCenterId = command.ControlCenterId;
        Active = command.Active;
        SetChannels(command.ChannelIds);
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the role.
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    ///     Gets the e-mail address.
    /// </summary>
    public string? Email { get; private set; }

    /// <summary>
    ///     Gets the phone number.
    /// </summary>
    public string? Phone { get; private set; }

    /// <summary>
    ///     Gets the identifier of the control center, if the recipient belongs to one.
    /// </summary>
    public int? ControlCenterId { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether the recipient is active.
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    ///     Gets the notification channels the recipient accepts.
    /// </summary>
    public IReadOnlyCollection<RecipientChannel> Channels => _channels;

    /// <summary>
    ///     Updates the recipient from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateRecipientCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        Role = command.Role;
        Email = command.Email;
        Phone = command.Phone;
        ControlCenterId = command.ControlCenterId;
        Active = command.Active;
        if (command.ChannelIds is not null) SetChannels(command.ChannelIds);
        Validate();
    }

    private void SetChannels(IEnumerable<int> channelIds)
    {
        var wanted = channelIds.Distinct().ToList();
        foreach (var channelId in wanted) Guard.AtLeast(channelId, 1, nameof(channelIds));

        _channels.RemoveAll(link => !wanted.Contains(link.ChannelId));
        foreach (var channelId in wanted.Where(id => _channels.All(link => link.ChannelId != id)))
            _channels.Add(new RecipientChannel(channelId));
    }

    private void Validate()
    {
        Guard.NotBlank(Name, nameof(Name));
        Guard.NotLongerThan(Name, 100, nameof(Name));
        Guard.NotBlank(Role, nameof(Role));
        Guard.NotLongerThan(Role, 80, nameof(Role));
        if (Email is not null) Guard.NotLongerThan(Email, 120, nameof(Email));
        if (Phone is not null) Guard.NotLongerThan(Phone, 20, nameof(Phone));
        if (ControlCenterId is not null) Guard.AtLeast(ControlCenterId.Value, 1, nameof(ControlCenterId));
    }
}
