using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete notification channel operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteNotificationChannelResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteNotificationChannelResult(
        Result<NotificationChannel, DeleteNotificationChannelError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<NotificationChannel, DeleteNotificationChannelError>.Success success =>
                controller.Ok(new object()),
            Result<NotificationChannel, DeleteNotificationChannelError>.Failure failure =>
                failure.Error switch
                {
                    DeleteNotificationChannelError.NotificationChannelNotFound =>
                        controller.NotFound(localizer["NotificationChannelNotFound"].Value),
                    DeleteNotificationChannelError.NotificationChannelHasRelatedData =>
                        controller.Conflict(localizer["NotificationChannelHasRelatedData"].Value),
                    DeleteNotificationChannelError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingNotificationChannel"].Value,
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
