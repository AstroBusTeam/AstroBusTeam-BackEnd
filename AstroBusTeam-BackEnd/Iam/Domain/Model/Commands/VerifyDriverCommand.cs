using SecurityBus.API.Iam.Domain.Model.ValueObjects;

namespace SecurityBus.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command to verify the identity of a driver who is starting a shift
/// </summary>
/// <param name="EmployeeCode">The employee code entered by the driver</param>
public record VerifyDriverCommand(EmployeeCode EmployeeCode);
