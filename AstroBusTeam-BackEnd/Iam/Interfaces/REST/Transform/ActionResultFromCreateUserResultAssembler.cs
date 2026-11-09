using AstroBusTeamBackEnd.Iam.Application.Errors;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create user operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateUserResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateUserResult(
        Result<User, CreateUserError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<User, CreateUserError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<User, CreateUserError>.Failure failure =>
                failure.Error switch
                {
                    CreateUserError.InvalidData =>
                        controller.BadRequest(localizer["InvalidUserData"].Value),
                    CreateUserError.DuplicateUsername =>
                        controller.Conflict(localizer["DuplicateUserUsername"].Value),
                    CreateUserError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidUserReference"].Value),
                    CreateUserError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingUser"].Value,
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
