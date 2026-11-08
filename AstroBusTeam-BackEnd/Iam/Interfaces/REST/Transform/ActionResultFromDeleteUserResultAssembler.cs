using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete user operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteUserResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteUserResult(
        Result<User, DeleteUserError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<User, DeleteUserError>.Success success =>
                controller.Ok(new object()),
            Result<User, DeleteUserError>.Failure failure =>
                failure.Error switch
                {
                    DeleteUserError.UserNotFound =>
                        controller.NotFound(localizer["UserNotFound"].Value),
                    DeleteUserError.UserHasRelatedData =>
                        controller.Conflict(localizer["UserHasRelatedData"].Value),
                    DeleteUserError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingUser"].Value,
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
