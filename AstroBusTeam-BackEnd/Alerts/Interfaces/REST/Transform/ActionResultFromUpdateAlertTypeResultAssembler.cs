using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update alert type operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateAlertTypeResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateAlertTypeResult(
        Result<AlertType, UpdateAlertTypeError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<AlertType, UpdateAlertTypeError>.Success success =>
                controller.Ok(AlertTypeResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<AlertType, UpdateAlertTypeError>.Failure failure =>
                failure.Error switch
                {
                    UpdateAlertTypeError.AlertTypeNotFound =>
                        controller.NotFound(localizer["AlertTypeNotFound"].Value),
                    UpdateAlertTypeError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAlertTypeData"].Value),
                    UpdateAlertTypeError.DuplicateName =>
                        controller.Conflict(localizer["DuplicateAlertTypeName"].Value),
                    UpdateAlertTypeError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingAlertType"].Value,
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
