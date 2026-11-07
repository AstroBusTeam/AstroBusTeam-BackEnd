namespace AstroBusTeamBackEnd.Iam.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing the employee code a driver uses to verify their identity.
/// </summary>
public sealed record EmployeeCode
{
    private const int MaxLength = 20;

    /// <summary>
    ///     Initializes a new <see cref="EmployeeCode"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw employee code. Must be non-null, non-whitespace, and at most 20 characters.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty, whitespace or too long.</exception>
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
