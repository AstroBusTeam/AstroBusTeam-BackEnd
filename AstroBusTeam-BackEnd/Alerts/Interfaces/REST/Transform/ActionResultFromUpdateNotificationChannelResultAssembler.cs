using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update notification channel operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateNotificationChannelResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateNotificationChannelResult(
        Result<NotificationChannel, UpdateNotificationChannelError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<NotificationChannel, UpdateNotificationChannelError>.Success success =>
                controller.Ok(NotificationChannelResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<NotificationChannel, UpdateNotificationChannelError>.Failure failure =>
                failure.Error switch
                {
                    UpdateNotificationChannelError.NotificationChannelNotFound =>
                        controller.NotFound(localizer["NotificationChannelNotFound"].Value),
                    UpdateNotificationChannelError.InvalidData =>
                        controller.BadRequest(localizer["InvalidNotificationChannelData"].Value),
                    UpdateNotificationChannelError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateNotificationChannelCode"].Value),
                    UpdateNotificationChannelError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingNotificationChannel"].Value,
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
