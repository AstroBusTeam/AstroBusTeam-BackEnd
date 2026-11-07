using SecurityBus.API.Iam.Application.Errors;
using SecurityBus.API.Iam.Domain.Model.Commands;
using SecurityBus.API.Iam.Domain.Model.ValueObjects;
using SecurityBus.API.Shared.Application.Patterns;

namespace SecurityBus.API.Iam.Application.Services;

/// <summary>
///     Command service interface for driver identity verification.
/// </summary>
/// <remarks>
///     A driver is verified when the employee code exists, the driver is active and has an active assignment.
///     This is the backend of the "identity verification" screen that starts a shift (TS01).
/// </remarks>
public interface IDriverVerificationCommandService
{
    /// <summary>
    ///     Handle the verify driver command.
    /// </summary>
    /// <param name="command">VerifyDriverCommand command</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A Result containing the verified driver identity on success, or the error reason on failure.
    /// </returns>
    Task<Result<DriverIdentity, VerifyDriverError>> Handle(VerifyDriverCommand command,
        CancellationToken cancellationToken = default);
}
