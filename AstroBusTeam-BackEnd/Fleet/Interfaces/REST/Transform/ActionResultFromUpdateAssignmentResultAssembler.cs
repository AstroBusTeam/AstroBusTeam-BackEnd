using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update assignment operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateAssignmentResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateAssignmentResult(
        Result<Assignment, UpdateAssignmentError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Assignment, UpdateAssignmentError>.Success success =>
                controller.Ok(AssignmentResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<Assignment, UpdateAssignmentError>.Failure failure =>
                failure.Error switch
                {
                    UpdateAssignmentError.AssignmentNotFound =>
                        controller.NotFound(localizer["AssignmentNotFound"].Value),
                    UpdateAssignmentError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAssignmentData"].Value),
                    UpdateAssignmentError.DriverAlreadyAssigned =>
                        controller.Conflict(localizer["DriverAlreadyAssigned"].Value),
                    UpdateAssignmentError.VehicleAlreadyAssigned =>
                        controller.Conflict(localizer["VehicleAlreadyAssigned"].Value),
                    UpdateAssignmentError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingAssignment"].Value,
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
