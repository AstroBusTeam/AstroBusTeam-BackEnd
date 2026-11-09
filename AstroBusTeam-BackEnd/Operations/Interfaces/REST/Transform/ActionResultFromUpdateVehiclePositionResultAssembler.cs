using AstroBusTeamBackEnd.Operations.Application.Errors;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update vehicle position operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateVehiclePositionResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateVehiclePositionResult(
        Result<VehiclePosition, UpdateVehiclePositionError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<VehiclePosition, UpdateVehiclePositionError>.Success success =>
                controller.Ok(VehiclePositionResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<VehiclePosition, UpdateVehiclePositionError>.Failure failure =>
                failure.Error switch
                {
                    UpdateVehiclePositionError.VehiclePositionNotFound =>
                        controller.NotFound(localizer["VehiclePositionNotFound"].Value),
                    UpdateVehiclePositionError.InvalidData =>
                        controller.BadRequest(localizer["InvalidVehiclePositionData"].Value),
                    UpdateVehiclePositionError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidVehiclePositionReference"].Value),
                    UpdateVehiclePositionError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingVehiclePosition"].Value,
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
