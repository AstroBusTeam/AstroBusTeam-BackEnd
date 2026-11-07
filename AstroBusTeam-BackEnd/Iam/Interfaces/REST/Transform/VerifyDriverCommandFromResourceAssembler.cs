using SecurityBus.API.Iam.Domain.Model.Commands;
using SecurityBus.API.Iam.Domain.Model.ValueObjects;
using SecurityBus.API.Iam.Interfaces.REST.Resources;

namespace SecurityBus.API.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembles a VerifyDriverCommand from a VerifyDriverResource.
/// </summary>
public static class VerifyDriverCommandFromResourceAssembler
{
    /// <summary>
    ///     Assembles a VerifyDriverCommand from a VerifyDriverResource.
    /// </summary>
    /// <param name="resource">The VerifyDriverResource resource</param>
    /// <returns>A VerifyDriverCommand assembled from the VerifyDriverResource</returns>
    /// <exception cref="ArgumentException">Thrown when the employee code is not valid.</exception>
    public static VerifyDriverCommand ToCommandFromResource(VerifyDriverResource resource) =>
        new(new EmployeeCode(resource.EmployeeCode));
}
