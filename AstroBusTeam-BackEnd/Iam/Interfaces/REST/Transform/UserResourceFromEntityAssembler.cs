using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a user entity into a resource
/// </summary>
public static class UserResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a resource.
    /// </summary>
    public static UserResource ToResourceFromEntity(User entity) =>
        new(entity.Id,
            entity.Username,
            entity.Role,
            entity.DriverId,
            entity.Active,
            entity.LastLoginAt,
            entity.CreatedAt);
}
