namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Links a recipient with a notification channel it accepts
/// </summary>
/// <remarks>
///     It is the join table <c>recipient_channels</c>. The recipient identifier is filled by Entity Framework Core
///     when the link is added to <see cref="Recipient.Channels"/>.
/// </remarks>
public class RecipientChannel
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected RecipientChannel()
    {
    }

    /// <summary>
    ///     Creates a link to a notification channel.
    /// </summary>
    /// <param name="channelId">The identifier of the notification channel.</param>
    public RecipientChannel(int channelId)
    {
        ChannelId = channelId;
    }

    /// <summary>
    ///     Gets the identifier of the recipient.
    /// </summary>
    public int RecipientId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the notification channel.
    /// </summary>
    public int ChannelId { get; private set; }
}
