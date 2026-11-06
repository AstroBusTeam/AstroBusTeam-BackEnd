using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to update a alert in the application layer.
/// </summary>
public static class ActionResultFromUpdateAlertResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a update alert operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromUpdateAlertResult(
        Result<Alert, UpdateAlertError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Alert, UpdateAlertError>.Success success =>
                controller.Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Alert, UpdateAlertError>.Failure failure =>
                failure.Error switch
                {
                    UpdateAlertError.AlertNotFound =>
                        controller.NotFound(localizer["AlertNotFound"].Value),

                    UpdateAlertError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAlertData"].Value),

                    UpdateAlertError.DuplicateAlertCode =>
                        controller.Conflict(localizer["DuplicateAlertCode"].Value),

                    UpdateAlertError.AttemptsCannotDecrease =>
                        controller.Conflict(localizer["AlertAttemptsCannotDecrease"].Value),

                    UpdateAlertError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingAlert"].Value,
                            statusCode: 500),

                    _ => controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                        statusCode: 500)
                },

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                statusCode: 500)
        };
}
