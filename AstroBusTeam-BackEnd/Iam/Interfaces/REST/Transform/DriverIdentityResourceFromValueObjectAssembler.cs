using SecurityBus.API.Iam.Domain.Model.ValueObjects;
using SecurityBus.API.Iam.Interfaces.REST.Resources;

namespace SecurityBus.API.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembles a DriverIdentityResource from a DriverIdentity value object.
/// </summary>
public static class DriverIdentityResourceFromValueObjectAssembler
{
    /// <summary>
    ///     Assembles a DriverIdentityResource from a DriverIdentity.
    /// </summary>
    /// <param name="identity">The DriverIdentity value object</param>
    /// <returns>A DriverIdentityResource assembled from the DriverIdentity</returns>
    public static DriverIdentityResource ToResourceFromValueObject(DriverIdentity identity) =>
        new(identity.DriverId, identity.EmployeeCode, identity.FullName, identity.VehicleId, identity.VehicleCode,
            identity.Route);
}
