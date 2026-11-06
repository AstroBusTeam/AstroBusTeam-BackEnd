namespace SecurityBus.API.Alerts.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated delivery status (sent or failed).
/// </summary>
/// <remarks>
///     Allowed values: "sent", "failed". The value is stored in lowercase so it matches the contract of the Fake API.
/// </remarks>
public sealed record DeliveryStatus
{
    private static readonly string[] AllowedValues = ["sent", "failed"];

    /// <summary>
    ///     Initializes a new <see cref="DeliveryStatus"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw status value (case-insensitive).</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not one of the allowed values.</exception>
    public DeliveryStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("DeliveryStatus cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();
        if (!AllowedValues.Contains(normalized))
            throw new ArgumentException($"DeliveryStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the "sent" status.
    /// </summary>
    public static DeliveryStatus Sent => new("sent");

    /// <summary>
    ///     Gets the "failed" status.
    /// </summary>
    public static DeliveryStatus Failed => new("failed");

    /// <summary>
    ///     Gets a value indicating whether the status is "sent".
    /// </summary>
    public bool IsSent => Value == "sent";

    /// <summary>
    ///     Gets a value indicating whether the status is "failed".
    /// </summary>
    public bool IsFailed => Value == "failed";

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
