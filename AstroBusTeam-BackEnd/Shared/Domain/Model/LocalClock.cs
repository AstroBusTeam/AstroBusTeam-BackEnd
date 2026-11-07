namespace AstroBusTeamBackEnd.Shared.Domain.Model;

/// <summary>
///     Provides the current local date and time with the same format the Fake API used: no offset and whole seconds.
/// </summary>
/// <remarks>
///     It is used to fill in the date and time of resources whose client did not send one. Values sent by the
///     client are stored as received.
/// </remarks>
public static class LocalClock
{
    /// <summary>
    ///     Gets the current local date and time truncated to whole seconds, with an unspecified kind.
    /// </summary>
    /// <returns>The current local date and time.</returns>
    public static DateTime Now()
    {
        var now = DateTime.Now;
        return new DateTime(now.Ticks - now.Ticks % TimeSpan.TicksPerSecond, DateTimeKind.Unspecified);
    }
}
