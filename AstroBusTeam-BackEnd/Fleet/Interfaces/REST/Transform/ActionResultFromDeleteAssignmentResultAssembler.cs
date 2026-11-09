using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to delete a assignment in the application layer.
/// </summary>
public static class ActionResultFromDeleteAssignmentResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a delete assignment operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromDeleteAssignmentResult(
        Result<Assignment, DeleteAssignmentError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Assignment, DeleteAssignmentError>.Success success =>
                controller.Ok(new object()),

            Result<Assignment, DeleteAssignmentError>.Failure failure =>
                failure.Error switch
                {
                    DeleteAssignmentError.AssignmentNotFound =>
                        controller.NotFound(localizer["AssignmentNotFound"].Value),

                    DeleteAssignmentError.AssignmentHasRelatedData =>
                        controller.Conflict(localizer["AssignmentHasRelatedData"].Value),
                    DeleteAssignmentError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingAssignment"].Value,
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
