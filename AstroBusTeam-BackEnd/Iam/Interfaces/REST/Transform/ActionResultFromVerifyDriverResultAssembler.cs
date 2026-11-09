using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to verify a driver in the application layer.
/// </summary>
public static class ActionResultFromVerifyDriverResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a verify driver operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromVerifyDriverResult(
        Result<DriverIdentity, VerifyDriverError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<DriverIdentity, VerifyDriverError>.Success success =>
                controller.Ok(DriverIdentityResourceFromValueObjectAssembler.ToResourceFromValueObject(success.Value)),

            Result<DriverIdentity, VerifyDriverError>.Failure failure =>
                failure.Error switch
                {
                    VerifyDriverError.DriverNotFound =>
                        controller.NotFound(localizer["VerifyDriverNotFound"].Value),

                    VerifyDriverError.DriverNotActive =>
                        controller.Problem(
                            title: localizer["VerifyDriverForbiddenTitle"].Value,
                            detail: localizer["VerifyDriverNotActive"].Value,
                            statusCode: 403),

                    VerifyDriverError.NoActiveAssignment =>
                        controller.Problem(
                            title: localizer["VerifyDriverForbiddenTitle"].Value,
                            detail: localizer["VerifyDriverNoActiveAssignment"].Value,
                            statusCode: 403),

                    VerifyDriverError.AssignedVehicleNotFound =>
                        controller.Problem(
                            title: localizer["VerifyDriverForbiddenTitle"].Value,
                            detail: localizer["VerifyDriverAssignedVehicleNotFound"].Value,
                            statusCode: 403),

                    _ => controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorVerifyingDriver"].Value,
                        statusCode: 500)
                },

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                statusCode: 500)
        };
}
