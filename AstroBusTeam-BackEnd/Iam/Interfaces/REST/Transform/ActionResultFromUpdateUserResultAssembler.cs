using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update user operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateUserResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateUserResult(
        Result<User, UpdateUserError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<User, UpdateUserError>.Success success =>
                controller.Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<User, UpdateUserError>.Failure failure =>
                failure.Error switch
                {
                    UpdateUserError.UserNotFound =>
                        controller.NotFound(localizer["UserNotFound"].Value),
                    UpdateUserError.InvalidData =>
                        controller.BadRequest(localizer["InvalidUserData"].Value),
                    UpdateUserError.DuplicateUsername =>
                        controller.Conflict(localizer["DuplicateUserUsername"].Value),
                    UpdateUserError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidUserReference"].Value),
                    UpdateUserError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingUser"].Value,
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
