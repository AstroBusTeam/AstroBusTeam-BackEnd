using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Shared.Domain.Model;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;

/// <summary>
///     Represents a control center of the SecurityBus platform
/// </summary>
public partial class ControlCenter
{
    /// <summary>
    ///     Required by Entity Framework Core.
    /// </summary>
    protected ControlCenter()
    {
        Code = string.Empty;
        Name = string.Empty;
        Zone = string.Empty;
    }

    /// <summary>
    ///     Creates a control center from a command.
    /// </summary>
    /// <param name="command">The command with the control center data.</param>
    public ControlCenter(CreateControlCenterCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Name = command.Name;
        Zone = command.Zone;
        Active = command.Active;
        Validate();
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    public int Id { get; private set; }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public string Zone { get; private set; }

    public bool Active { get; private set; }

    /// <summary>
    ///     Updates the control center from a command.
    /// </summary>
    /// <param name="command">The command with the new data.</param>
    public void Update(UpdateControlCenterCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        Code = command.Code;
        Name = command.Name;
        Zone = command.Zone;
        Active = command.Active;
        Validate();
    }

    private void Validate()
    {
        Guard.NotBlank(Code, nameof(Code));
        Guard.NotLongerThan(Code, 40, nameof(Code));
        Guard.NotBlank(Name, nameof(Name));
        Guard.NotLongerThan(Name, 100, nameof(Name));
        Guard.NotBlank(Zone, nameof(Zone));
        Guard.NotLongerThan(Zone, 60, nameof(Zone));
    }
}
