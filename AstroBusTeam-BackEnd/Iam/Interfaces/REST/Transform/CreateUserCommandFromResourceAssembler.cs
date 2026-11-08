using AstroBusTeamBackEnd.Iam.Domain.Services;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create user resource into a command
/// </summary>
public static class CreateUserCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) =>
        new CreateUserCommand(
            resource.Username,
            PasswordHasher.Hash(resource.Password),
            resource.Role ?? "operator",
            resource.DriverId,
            resource.Active ?? true);
}
