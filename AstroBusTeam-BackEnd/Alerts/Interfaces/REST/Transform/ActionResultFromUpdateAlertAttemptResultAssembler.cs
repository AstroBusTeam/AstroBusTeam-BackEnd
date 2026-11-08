using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update alert attempt operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateAlertAttemptResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateAlertAttemptResult(
        Result<AlertAttempt, UpdateAlertAttemptError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<AlertAttempt, UpdateAlertAttemptError>.Success success =>
                controller.Ok(AlertAttemptResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<AlertAttempt, UpdateAlertAttemptError>.Failure failure =>
                failure.Error switch
                {
                    UpdateAlertAttemptError.AlertAttemptNotFound =>
                        controller.NotFound(localizer["AlertAttemptNotFound"].Value),
                    UpdateAlertAttemptError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAlertAttemptData"].Value),
                    UpdateAlertAttemptError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidAlertAttemptReference"].Value),
                    UpdateAlertAttemptError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingAlertAttempt"].Value,
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
