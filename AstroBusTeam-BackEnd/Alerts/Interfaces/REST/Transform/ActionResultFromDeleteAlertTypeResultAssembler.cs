using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete alert type operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteAlertTypeResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteAlertTypeResult(
        Result<AlertType, DeleteAlertTypeError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<AlertType, DeleteAlertTypeError>.Success success =>
                controller.Ok(new object()),
            Result<AlertType, DeleteAlertTypeError>.Failure failure =>
                failure.Error switch
                {
                    DeleteAlertTypeError.AlertTypeNotFound =>
                        controller.NotFound(localizer["AlertTypeNotFound"].Value),
                    DeleteAlertTypeError.AlertTypeHasRelatedData =>
                        controller.Conflict(localizer["AlertTypeHasRelatedData"].Value),
                    DeleteAlertTypeError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingAlertType"].Value,
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
