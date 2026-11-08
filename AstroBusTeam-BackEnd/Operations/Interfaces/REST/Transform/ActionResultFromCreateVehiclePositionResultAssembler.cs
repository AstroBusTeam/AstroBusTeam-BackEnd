using AstroBusTeamBackEnd.Operations.Application.Errors;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create vehicle position operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateVehiclePositionResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateVehiclePositionResult(
        Result<VehiclePosition, CreateVehiclePositionError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<VehiclePosition, CreateVehiclePositionError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    VehiclePositionResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<VehiclePosition, CreateVehiclePositionError>.Failure failure =>
                failure.Error switch
                {
                    CreateVehiclePositionError.InvalidData =>
                        controller.BadRequest(localizer["InvalidVehiclePositionData"].Value),
                    CreateVehiclePositionError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidVehiclePositionReference"].Value),
                    CreateVehiclePositionError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingVehiclePosition"].Value,
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
