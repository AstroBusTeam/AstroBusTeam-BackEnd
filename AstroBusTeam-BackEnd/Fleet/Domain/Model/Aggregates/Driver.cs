using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a driver of the fleet
/// </summary>
/// <remarks>
///     The driver keeps its personal and license data. The license category is one of the Peruvian categories
///     listed in <see cref="LicenseCategories"/>.
/// </remarks>
public partial class Driver
{
    /// <summary>
    ///     The license categories accepted for a driver.
    /// </summary>
    public static readonly string[] LicenseCategories = ["A-I", "A-IIa", "A-IIb", "A-IIIa", "A-IIIb", "A-IIIc"];

    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected Driver()
    {
        EmployeeCode = null!;
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = null!;
        LicenseCategory = string.Empty;
        LicenseNumber = null!;
        Status = null!;
    }

    /// <summary>
    ///     Creates a driver from a command.
    /// </summary>
    /// <param name="command">The command with the driver data.</param>
    public Driver(CreateDriverCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        EmployeeCode = command.EmployeeCode;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        LicenseCategory = CanonicalCategory(command.LicenseCategory);
        LicenseNumber = command.LicenseNumber;
        LicenseExpiry = command.LicenseExpiry;
        Points = command.Points;
        Rating = command.Rating;
        YearsExperience = command.YearsExperience;
        Phone = command.Phone;
        Status = command.Status;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the employee code.
    /// </summary>
    public EmployeeCode EmployeeCode { get; private set; }

    /// <summary>
    ///     Gets the first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    ///     Gets the last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    ///     Gets the national identity document number.
    /// </summary>
    public Dni Dni { get; private set; }

    /// <summary>
    ///     Gets the license category.
    /// </summary>
    public string LicenseCategory { get; private set; }

    /// <summary>
    ///     Gets the license number.
    /// </summary>
    public LicenseNumber LicenseNumber { get; private set; }

    /// <summary>
    ///     Gets the license expiry date.
    /// </summary>
    public DateOnly LicenseExpiry { get; private set; }

    /// <summary>
    ///     Gets the license points.
    /// </summary>
    public int Points { get; private set; }

    /// <summary>
    ///     Gets the rating from 0 to 5.
    /// </summary>
    public decimal Rating { get; private set; }

    /// <summary>
    ///     Gets the years of experience.
    /// </summary>
    public int YearsExperience { get; private set; }

    /// <summary>
    ///     Gets the contact phone number.
    /// </summary>
    public string? Phone { get; private set; }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    public DriverStatus Status { get; private set; }

    /// <summary>
    ///     Updates the driver from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateDriverCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        EmployeeCode = command.EmployeeCode;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        LicenseCategory = CanonicalCategory(command.LicenseCategory);
        LicenseNumber = command.LicenseNumber;
        LicenseExpiry = command.LicenseExpiry;
        Points = command.Points;
        Rating = command.Rating;
        YearsExperience = command.YearsExperience;
        Phone = command.Phone;
        Status = command.Status;
        Validate();
    }

    private static string CanonicalCategory(string? value)
    {
        var trimmed = value?.Trim() ?? string.Empty;
        return LicenseCategories.FirstOrDefault(c => string.Equals(c, trimmed, StringComparison.OrdinalIgnoreCase)) ??
               trimmed;
    }

    private void Validate()
    {
        Guard.NotBlank(FirstName, nameof(FirstName));
        Guard.NotLongerThan(FirstName, 80, nameof(FirstName));
        Guard.NotBlank(LastName, nameof(LastName));
        Guard.NotLongerThan(LastName, 80, nameof(LastName));
        Guard.OneOf(LicenseCategory, LicenseCategories, nameof(LicenseCategory));
        Guard.InRange(Points, 0, 32767, nameof(Points));
        Guard.InRange(Rating, 0m, 5m, nameof(Rating));
        Guard.InRange(YearsExperience, 0, 32767, nameof(YearsExperience));
        if (Phone is not null) Guard.NotLongerThan(Phone, 20, nameof(Phone));
    }
}
