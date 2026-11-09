using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update control center operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateControlCenterResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateControlCenterResult(
        Result<ControlCenter, UpdateControlCenterError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<ControlCenter, UpdateControlCenterError>.Success success =>
                controller.Ok(ControlCenterResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<ControlCenter, UpdateControlCenterError>.Failure failure =>
                failure.Error switch
                {
                    UpdateControlCenterError.ControlCenterNotFound =>
                        controller.NotFound(localizer["ControlCenterNotFound"].Value),
                    UpdateControlCenterError.InvalidData =>
                        controller.BadRequest(localizer["InvalidControlCenterData"].Value),
                    UpdateControlCenterError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateControlCenterCode"].Value),
                    UpdateControlCenterError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingControlCenter"].Value,
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
