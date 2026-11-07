using System.Net.Mime;
using SecurityBus.API.Iam.Application.Services;
using SecurityBus.API.Iam.Interfaces.REST.Resources;
using SecurityBus.API.Iam.Interfaces.REST.Transform;
using SecurityBus.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Iam.Interfaces.REST;

/// <summary>
///     Authentication controller. Verifies the identity of a driver before a shift starts.
/// </summary>
/// <param name="driverVerificationCommandService">The Driver Verification Command Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Authentication")]
public class AuthenticationController(
    IDriverVerificationCommandService driverVerificationCommandService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<AuthenticationController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Verifies the identity of a driver by employee code.
    /// </summary>
    /// <param name="resource">VerifyDriverResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the verified identity, or an error response.
    /// </returns>
    [HttpPost("driver-verification")]
    [ProducesResponseType(typeof(DriverIdentityResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> VerifyDriver([FromBody] VerifyDriverResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = VerifyDriverCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await driverVerificationCommandService.Handle(command, cancellationToken);
            return ActionResultFromVerifyDriverResultAssembler.ToActionResultFromVerifyDriverResult(result, this,
                localizer);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while verifying a driver identity");
            return BadRequest(localizer["InvalidVerifyDriverRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while verifying a driver identity");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorVerifyingDriver"].Value,
                statusCode: 500);
        }
    }
}
