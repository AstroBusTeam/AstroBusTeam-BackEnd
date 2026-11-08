using AstroBusTeamBackEnd.Operations.Application.Errors;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete vehicle position operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteVehiclePositionResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteVehiclePositionResult(
        Result<VehiclePosition, DeleteVehiclePositionError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<VehiclePosition, DeleteVehiclePositionError>.Success success =>
                controller.Ok(new object()),
            Result<VehiclePosition, DeleteVehiclePositionError>.Failure failure =>
                failure.Error switch
                {
                    DeleteVehiclePositionError.VehiclePositionNotFound =>
                        controller.NotFound(localizer["VehiclePositionNotFound"].Value),
                    DeleteVehiclePositionError.VehiclePositionHasRelatedData =>
                        controller.Conflict(localizer["VehiclePositionHasRelatedData"].Value),
                    DeleteVehiclePositionError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingVehiclePosition"].Value,
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
