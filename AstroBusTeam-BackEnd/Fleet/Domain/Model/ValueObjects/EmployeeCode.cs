namespace AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated employee code.
/// </summary>
public sealed record EmployeeCode
{
    private const int MaxLength = 20;

    /// <summary>
    ///     Initializes a new <see cref="EmployeeCode"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw value. Must be non-null, non-whitespace, and at most 20 characters.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty, whitespace, exceeds the maximum length or has an invalid format.</exception>
    public EmployeeCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("EmployeeCode cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToUpperInvariant();

        if (normalized.Length > MaxLength)
            throw new ArgumentException($"EmployeeCode cannot be longer than {MaxLength} characters.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
