using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;


namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a alert type of the SecurityBus platform
/// </summary>
public partial class AlertType
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected AlertType()
    {
        Name = string.Empty;
        DefaultPriority = string.Empty;
    }

    /// <summary>
    ///     Creates a alert type from a command.
    /// </summary>
    /// <param name="command">The command with the alert type data.</param>
    public AlertType(CreateAlertTypeCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        DefaultPriority = command.DefaultPriority.Trim().ToLowerInvariant();
        IsPanic = command.IsPanic;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string DefaultPriority { get; private set; }

    public bool IsPanic { get; private set; }

    /// <summary>
    ///     Updates the alert type from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateAlertTypeCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Name = command.Name;
        DefaultPriority = command.DefaultPriority.Trim().ToLowerInvariant();
        IsPanic = command.IsPanic;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Name, nameof(Name));
        Guard.NotLongerThan(Name, 60, nameof(Name));
        Guard.OneOf(DefaultPriority, ["low", "medium", "high", "critical"], nameof(DefaultPriority));
    }
}
