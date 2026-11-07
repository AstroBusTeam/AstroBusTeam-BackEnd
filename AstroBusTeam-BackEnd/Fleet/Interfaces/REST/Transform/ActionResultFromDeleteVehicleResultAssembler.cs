using SecurityBus.API.Fleet.Application.Errors;
using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to delete a vehicle in the application layer.
/// </summary>
public static class ActionResultFromDeleteVehicleResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a delete vehicle operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromDeleteVehicleResult(
        Result<Vehicle, DeleteVehicleError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Vehicle, DeleteVehicleError>.Success success =>
                controller.Ok(new object()),

            Result<Vehicle, DeleteVehicleError>.Failure failure =>
                failure.Error switch
                {
                    DeleteVehicleError.VehicleNotFound =>
                        controller.NotFound(localizer["VehicleNotFound"].Value),

                    DeleteVehicleError.VehicleHasActiveAssignment =>
                        controller.Conflict(localizer["VehicleHasActiveAssignment"].Value),

                    DeleteVehicleError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingVehicle"].Value,
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
