using System.Text.RegularExpressions;

namespace SecurityBus.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated national id (dni).
/// </summary>
public sealed record Dni
{
    private const int MaxLength = 8;

    /// <summary>
    ///     Initializes a new <see cref="Dni"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw value. Must be non-null, non-whitespace, and at most 8 characters.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty, whitespace, exceeds the maximum length or has an invalid format.</exception>
    public Dni(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Dni cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim();

        if (normalized.Length > MaxLength)
            throw new ArgumentException($"Dni cannot be longer than {MaxLength} characters.", nameof(value));

        if (!Regex.IsMatch(normalized, @"^\d{8}$"))
            throw new ArgumentException("Dni has an invalid format.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;

    /// <summary>
    ///     Returns a masked version of the value for logging purposes, showing only the last 2 characters.
    /// </summary>
    /// <returns>A string with all but the last 2 characters replaced by asterisks.</returns>
    public string MaskedForLogging() => MaskedForLogging(Value);

    /// <summary>
    ///     Masks a raw value for logging, including values that have not passed validation.
    /// </summary>
    /// <param name="value">The raw value to mask.</param>
    /// <returns>The masked value, showing only its last two characters when available.</returns>
    public static string MaskedForLogging(string? value) =>
        string.IsNullOrEmpty(value)
            ? string.Empty
            : value.Length <= 2
                ? new string('*', value.Length)
                : new string('*', value.Length - 2) + value[^2..];
}
