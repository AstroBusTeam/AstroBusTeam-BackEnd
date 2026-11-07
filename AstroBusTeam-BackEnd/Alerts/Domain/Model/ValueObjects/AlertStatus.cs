namespace AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated alert status (pending, confirmed or critical).
/// </summary>
/// <remarks>
///     Allowed values: "pending", "confirmed", "critical". The value is stored in lowercase so it matches the contract of the Fake API.
/// </remarks>
public sealed record AlertStatus
{
    private static readonly string[] AllowedValues = ["pending", "confirmed", "critical"];

    /// <summary>
    ///     Initializes a new <see cref="AlertStatus"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw status value (case-insensitive).</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not one of the allowed values.</exception>
    public AlertStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AlertStatus cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();
        if (!AllowedValues.Contains(normalized))
            throw new ArgumentException($"AlertStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the "pending" status.
    /// </summary>
    public static AlertStatus Pending => new("pending");

    /// <summary>
    ///     Gets the "confirmed" status.
    /// </summary>
    public static AlertStatus Confirmed => new("confirmed");

    /// <summary>
    ///     Gets the "critical" status.
    /// </summary>
    public static AlertStatus Critical => new("critical");

    /// <summary>
    ///     Gets a value indicating whether the status is "pending".
    /// </summary>
    public bool IsPending => Value == "pending";

    /// <summary>
    ///     Gets a value indicating whether the status is "confirmed".
    /// </summary>
    public bool IsConfirmed => Value == "confirmed";

    /// <summary>
    ///     Gets a value indicating whether the status is "critical".
    /// </summary>
    public bool IsCritical => Value == "critical";

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
