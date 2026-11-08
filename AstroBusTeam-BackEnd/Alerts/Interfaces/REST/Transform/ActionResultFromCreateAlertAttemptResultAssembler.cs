using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create alert attempt operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateAlertAttemptResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateAlertAttemptResult(
        Result<AlertAttempt, CreateAlertAttemptError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<AlertAttempt, CreateAlertAttemptError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    AlertAttemptResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<AlertAttempt, CreateAlertAttemptError>.Failure failure =>
                failure.Error switch
                {
                    CreateAlertAttemptError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAlertAttemptData"].Value),
                    CreateAlertAttemptError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidAlertAttemptReference"].Value),
                    CreateAlertAttemptError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingAlertAttempt"].Value,
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
