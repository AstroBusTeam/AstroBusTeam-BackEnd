using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

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
