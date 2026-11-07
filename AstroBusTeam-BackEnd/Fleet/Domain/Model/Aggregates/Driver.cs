using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;

/// <summary>
///     Represents a driver in the Fleet bounded context.
/// </summary>
/// <remarks>
///     This aggregate root protects its invariants: every creation and update goes through the same validation.
///     Audit timestamps (CreatedAt, UpdatedAt) are managed by the persistence layer.
/// </remarks>
public partial class Driver
{
    /// <summary>
    ///     Protected parameterless constructor for EF Core.
    /// </summary>
    protected Driver()
    {
        EmployeeCode = null!;
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = null!;
        Category = string.Empty;
        LicenseNumber = null!;
        Status = null!;
    }

    /// <summary>
    ///     Creates a new Driver from a creation command.
    /// </summary>
    /// <remarks>
    ///     This constructor acts as the command handler for CreateDriverCommand.
    /// </remarks>
    /// <param name="command">The CreateDriverCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public Driver(CreateDriverCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        EmployeeCode = command.EmployeeCode;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        Category = command.Category;
        LicenseNumber = command.LicenseNumber;
        LicenseExpiry = command.LicenseExpiry;
        Points = command.Points;
        Rating = command.Rating;
        YearsExperience = command.YearsExperience;
        Status = command.Status;
        Validate();
    }

    /// <summary>
    ///     Gets the server-generated identifier for this driver.
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
    ///     Gets the national ID (DNI).
    /// </summary>
    public Dni Dni { get; private set; }

    /// <summary>
    ///     Gets the license category.
    /// </summary>
    public string Category { get; private set; }

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
    ///     Gets the driver status (active or inactive).
    /// </summary>
    public DriverStatus Status { get; private set; }

    /// <summary>
    ///     Updates the driver with the values of an update command.
    /// </summary>
    /// <remarks>
    ///     This method acts as the command handler for UpdateDriverCommand. The invariants are validated again.
    /// </remarks>
    /// <param name="command">The UpdateDriverCommand command</param>
    /// <exception cref="ArgumentNullException">Thrown when command is null.</exception>
    public void Update(UpdateDriverCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        EmployeeCode = command.EmployeeCode;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        Category = command.Category;
        LicenseNumber = command.LicenseNumber;
        LicenseExpiry = command.LicenseExpiry;
        Points = command.Points;
        Rating = command.Rating;
        YearsExperience = command.YearsExperience;
        Status = command.Status;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(FirstName, nameof(FirstName));
        Guard.NotLongerThan(FirstName, 100, nameof(FirstName));
        Guard.NotBlank(LastName, nameof(LastName));
        Guard.NotLongerThan(LastName, 100, nameof(LastName));
        Guard.NotBlank(Category, nameof(Category));
        Guard.NotLongerThan(Category, 10, nameof(Category));
        Guard.AtLeast(Points, 0, nameof(Points));
        Guard.InRange(Rating, 0m, 5m, nameof(Rating));
        Guard.AtLeast(YearsExperience, 0, nameof(YearsExperience));
    }
}
