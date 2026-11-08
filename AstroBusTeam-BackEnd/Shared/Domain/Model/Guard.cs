namespace AstroBusTeamBackEnd.Shared.Domain.Model;

/// <summary>
///     Small guard clauses used by aggregates and value objects to protect their invariants.
/// </summary>
/// <remarks>
///     All guards throw <see cref="ArgumentException"/> (or a derived type) so that the REST layer can translate
///     them into a 400 Bad Request response.
/// </remarks>
public static class Guard
{
    /// <summary>
    ///     Ensures a string is neither null, empty nor whitespace.
    /// </summary>
    public static void NotBlank(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} cannot be null, empty, or whitespace.", name);
    }

    /// <summary>
    ///     Ensures a string does not exceed a maximum length.
    /// </summary>
    public static void NotLongerThan(string value, int maxLength, string name)
    {
        if (value.Length > maxLength)
            throw new ArgumentException($"{name} cannot be longer than {maxLength} characters.", name);
    }

    /// <summary>
    ///     Ensures a value is greater than or equal to a minimum.
    /// </summary>
    public static void AtLeast<T>(T value, T min, string name) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            throw new ArgumentOutOfRangeException(name, value, $"{name} must be greater than or equal to {min}.");
    }

    /// <summary>
    ///     Ensures a value lies within an inclusive range.
    /// </summary>
    public static void InRange<T>(T value, T min, T max, string name) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(name, value, $"{name} must be between {min} and {max}.");
    }

    /// <summary>
    ///     Ensures that a string is one of the allowed values.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="allowed">The allowed values.</param>
    /// <param name="name">The parameter name used in the exception.</param>
    /// <exception cref="ArgumentException">Thrown when the value is not one of the allowed values.</exception>
    public static void OneOf(string? value, string[] allowed, string name)
    {
        if (value is null || !allowed.Contains(value))
            throw new ArgumentException($"{name} must be one of: {string.Join(", ", allowed)}.", name);
    }
}
