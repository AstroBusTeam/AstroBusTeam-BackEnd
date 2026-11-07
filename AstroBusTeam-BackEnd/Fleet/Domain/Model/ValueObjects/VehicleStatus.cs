namespace SecurityBus.API.Fleet.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated vehicle status (available, in-service or maintenance).
/// </summary>
/// <remarks>
///     Allowed values: "available", "in-service", "maintenance". The value is stored in lowercase so it matches the contract of the Fake API.
/// </remarks>
public sealed record VehicleStatus
{
    private static readonly string[] AllowedValues = ["available", "in-service", "maintenance"];

    /// <summary>
    ///     Initializes a new <see cref="VehicleStatus"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw status value (case-insensitive).</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not one of the allowed values.</exception>
    public VehicleStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("VehicleStatus cannot be null, empty, or whitespace.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();
        if (!AllowedValues.Contains(normalized))
            throw new ArgumentException($"VehicleStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));

        Value = normalized;
    }

    /// <summary>
    ///     Gets the "available" status.
    /// </summary>
    public static VehicleStatus Available => new("available");

    /// <summary>
    ///     Gets the "in-service" status.
    /// </summary>
    public static VehicleStatus InService => new("in-service");

    /// <summary>
    ///     Gets the "maintenance" status.
    /// </summary>
    public static VehicleStatus Maintenance => new("maintenance");

    /// <summary>
    ///     Gets a value indicating whether the status is "available".
    /// </summary>
    public bool IsAvailable => Value == "available";

    /// <summary>
    ///     Gets a value indicating whether the status is "in-service".
    /// </summary>
    public bool IsInService => Value == "in-service";

    /// <summary>
    ///     Gets a value indicating whether the status is "maintenance".
    /// </summary>
    public bool IsMaintenance => Value == "maintenance";

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}
