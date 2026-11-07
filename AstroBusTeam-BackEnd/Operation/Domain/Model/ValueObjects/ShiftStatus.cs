namespace SecurityBus.API.Operations.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated shift status (active, finished or interrupted).
/// </summary>
/// <remarks>
///     Allowed values: "active", "finished", "interrupted". The value is stored in lowercase so it matches the contract of the Fake API.
/// </remarks>
public sealed record ShiftStatus
{
    private static readonly string[] AllowedValues = ["active", "finished", "interrupted"];

    /// <summary>
    ///     Initializes a new <see cref="ShiftStatus"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw status value (case-insensitive).</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not one of the allowed values.</exception>
    public ShiftStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ShiftStatus cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();
        if (!AllowedValues.Contains(normalized))
            throw new ArgumentException($"ShiftStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the "active" status.
    /// </summary>
    public static ShiftStatus Active => new("active");

    /// <summary>
    ///     Gets the "finished" status.
    /// </summary>
    public static ShiftStatus Finished => new("finished");

    /// <summary>
    ///     Gets the "interrupted" status.
    /// </summary>
    public static ShiftStatus Interrupted => new("interrupted");

    /// <summary>
    ///     Gets a value indicating whether the status is "active".
    /// </summary>
    public bool IsActive => Value == "active";

    /// <summary>
    ///     Gets a value indicating whether the status is "finished".
    /// </summary>
    public bool IsFinished => Value == "finished";

    /// <summary>
    ///     Gets a value indicating whether the status is "interrupted".
    /// </summary>
    public bool IsInterrupted => Value == "interrupted";

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
