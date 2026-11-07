using System.ComponentModel.DataAnnotations;

namespace SecurityBus.API.Iam.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to verify the identity of a driver.
/// </summary>
/// <param name="EmployeeCode">The employee code of the driver (required)</param>
public record VerifyDriverResource(
    [Required(ErrorMessage = "RequiredVerifyDriverEmployeeCode")]
    string EmployeeCode);
