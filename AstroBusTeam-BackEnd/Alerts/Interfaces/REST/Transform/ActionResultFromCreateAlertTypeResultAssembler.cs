using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create alert type operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateAlertTypeResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateAlertTypeResult(
        Result<AlertType, CreateAlertTypeError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<AlertType, CreateAlertTypeError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    AlertTypeResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<AlertType, CreateAlertTypeError>.Failure failure =>
                failure.Error switch
                {
                    CreateAlertTypeError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAlertTypeData"].Value),
                    CreateAlertTypeError.DuplicateName =>
                        controller.Conflict(localizer["DuplicateAlertTypeName"].Value),
                    CreateAlertTypeError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingAlertType"].Value,
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
