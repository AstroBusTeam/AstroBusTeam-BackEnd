using AstroBusTeamBackEnd.Iam.Domain.Services;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update user resource into a command
/// </summary>
public static class UpdateUserCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource) =>
        new UpdateUserCommand(
            id,
            resource.Username,
            resource.Password is null ? null : PasswordHasher.Hash(resource.Password),
            resource.Role,
            resource.DriverId,
            resource.Active);
}
