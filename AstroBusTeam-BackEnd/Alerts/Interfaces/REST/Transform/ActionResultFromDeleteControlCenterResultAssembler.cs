using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete control center operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteControlCenterResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteControlCenterResult(
        Result<ControlCenter, DeleteControlCenterError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<ControlCenter, DeleteControlCenterError>.Success success =>
                controller.Ok(new object()),
            Result<ControlCenter, DeleteControlCenterError>.Failure failure =>
                failure.Error switch
                {
                    DeleteControlCenterError.ControlCenterNotFound =>
                        controller.NotFound(localizer["ControlCenterNotFound"].Value),
                    DeleteControlCenterError.ControlCenterHasRelatedData =>
                        controller.Conflict(localizer["ControlCenterHasRelatedData"].Value),
                    DeleteControlCenterError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingControlCenter"].Value,
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
