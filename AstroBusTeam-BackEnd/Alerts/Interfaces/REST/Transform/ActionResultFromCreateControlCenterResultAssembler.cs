using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create control center operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateControlCenterResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateControlCenterResult(
        Result<ControlCenter, CreateControlCenterError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<ControlCenter, CreateControlCenterError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    ControlCenterResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<ControlCenter, CreateControlCenterError>.Failure failure =>
                failure.Error switch
                {
                    CreateControlCenterError.InvalidData =>
                        controller.BadRequest(localizer["InvalidControlCenterData"].Value),
                    CreateControlCenterError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateControlCenterCode"].Value),
                    CreateControlCenterError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingControlCenter"].Value,
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
