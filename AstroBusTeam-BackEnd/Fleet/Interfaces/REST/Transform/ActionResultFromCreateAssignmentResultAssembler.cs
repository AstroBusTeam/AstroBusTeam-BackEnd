using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to create a assignment in the application layer.
/// </summary>
public static class ActionResultFromCreateAssignmentResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a create assignment operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <param name="getByIdActionName">The name of the action used to generate the location header.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromCreateAssignmentResult(
        Result<Assignment, CreateAssignmentError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<Assignment, CreateAssignmentError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    AssignmentResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Assignment, CreateAssignmentError>.Failure failure =>
                failure.Error switch
                {
                    CreateAssignmentError.InvalidData =>
                        controller.BadRequest(localizer["InvalidAssignmentData"].Value),

                    CreateAssignmentError.DriverNotFound =>
                        controller.BadRequest(localizer["AssignmentDriverNotFound"].Value),

                    CreateAssignmentError.VehicleNotFound =>
                        controller.BadRequest(localizer["AssignmentVehicleNotFound"].Value),

                    CreateAssignmentError.DriverNotActive =>
                        controller.Conflict(localizer["AssignmentDriverNotActive"].Value),

                    CreateAssignmentError.VehicleInMaintenance =>
                        controller.Conflict(localizer["AssignmentVehicleInMaintenance"].Value),

                    CreateAssignmentError.DriverAlreadyAssigned =>
                        controller.Conflict(localizer["DriverAlreadyAssigned"].Value),

                    CreateAssignmentError.VehicleAlreadyAssigned =>
                        controller.Conflict(localizer["VehicleAlreadyAssigned"].Value),

                    CreateAssignmentError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingAssignment"].Value,
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
