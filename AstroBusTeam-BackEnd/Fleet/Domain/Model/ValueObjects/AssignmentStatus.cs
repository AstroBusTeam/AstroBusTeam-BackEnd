namespace SecurityBus.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated assignment status (active or inactive).
/// </summary>
/// <remarks>
///     Allowed values: "active", "inactive". The value is stored in lowercase so it matches the contract of the Fake API.
/// </remarks>
public sealed record AssignmentStatus
{
    private static readonly string[] AllowedValues = ["active", "inactive"];

    /// <summary>
    ///     Initializes a new <see cref="AssignmentStatus"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw status value (case-insensitive).</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not one of the allowed values.</exception>
    public AssignmentStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AssignmentStatus cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();
        if (!AllowedValues.Contains(normalized))
            throw new ArgumentException($"AssignmentStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the "active" status.
    /// </summary>
    public static AssignmentStatus Active => new("active");

    /// <summary>
    ///     Gets the "inactive" status.
    /// </summary>
    public static AssignmentStatus Inactive => new("inactive");

    /// <summary>
    ///     Gets a value indicating whether the status is "active".
    /// </summary>
    public bool IsActive => Value == "active";

    /// <summary>
    ///     Gets a value indicating whether the status is "inactive".
    /// </summary>
    public bool IsInactive => Value == "inactive";

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
