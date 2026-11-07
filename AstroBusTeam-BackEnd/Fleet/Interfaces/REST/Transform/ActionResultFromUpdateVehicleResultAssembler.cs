using SecurityBus.API.Fleet.Application.Errors;
using SecurityBus.API.Fleet.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to update a vehicle in the application layer.
/// </summary>
public static class ActionResultFromUpdateVehicleResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a update vehicle operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromUpdateVehicleResult(
        Result<Vehicle, UpdateVehicleError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Vehicle, UpdateVehicleError>.Success success =>
                controller.Ok(VehicleResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Vehicle, UpdateVehicleError>.Failure failure =>
                failure.Error switch
                {
                    UpdateVehicleError.VehicleNotFound =>
                        controller.NotFound(localizer["VehicleNotFound"].Value),

                    UpdateVehicleError.InvalidData =>
                        controller.BadRequest(localizer["InvalidVehicleData"].Value),

                    UpdateVehicleError.DuplicateVehicleCode =>
                        controller.Conflict(localizer["DuplicateVehicleCode"].Value),

                    UpdateVehicleError.DuplicateLicensePlate =>
                        controller.Conflict(localizer["DuplicateLicensePlate"].Value),

                    UpdateVehicleError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingVehicle"].Value,
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
