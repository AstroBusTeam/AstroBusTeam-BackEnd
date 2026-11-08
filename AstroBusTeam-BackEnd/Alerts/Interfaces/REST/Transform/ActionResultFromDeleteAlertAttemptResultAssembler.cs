using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete alert attempt operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteAlertAttemptResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteAlertAttemptResult(
        Result<AlertAttempt, DeleteAlertAttemptError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<AlertAttempt, DeleteAlertAttemptError>.Success success =>
                controller.Ok(new object()),
            Result<AlertAttempt, DeleteAlertAttemptError>.Failure failure =>
                failure.Error switch
                {
                    DeleteAlertAttemptError.AlertAttemptNotFound =>
                        controller.NotFound(localizer["AlertAttemptNotFound"].Value),
                    DeleteAlertAttemptError.AlertAttemptHasRelatedData =>
                        controller.Conflict(localizer["AlertAttemptHasRelatedData"].Value),
                    DeleteAlertAttemptError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingAlertAttempt"].Value,
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
