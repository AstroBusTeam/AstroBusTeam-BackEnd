using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create notification channel operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateNotificationChannelResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateNotificationChannelResult(
        Result<NotificationChannel, CreateNotificationChannelError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<NotificationChannel, CreateNotificationChannelError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    NotificationChannelResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<NotificationChannel, CreateNotificationChannelError>.Failure failure =>
                failure.Error switch
                {
                    CreateNotificationChannelError.InvalidData =>
                        controller.BadRequest(localizer["InvalidNotificationChannelData"].Value),
                    CreateNotificationChannelError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateNotificationChannelCode"].Value),
                    CreateNotificationChannelError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingNotificationChannel"].Value,
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
