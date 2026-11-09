using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to create a vehicle in the application layer.
/// </summary>
public static class ActionResultFromCreateVehicleResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a create vehicle operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <param name="getByIdActionName">The name of the action used to generate the location header.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromCreateVehicleResult(
        Result<Vehicle, CreateVehicleError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<Vehicle, CreateVehicleError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    VehicleResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Vehicle, CreateVehicleError>.Failure failure =>
                failure.Error switch
                {
                    CreateVehicleError.InvalidData =>
                        controller.BadRequest(localizer["InvalidVehicleData"].Value),

                    CreateVehicleError.DuplicateVehicleCode =>
                        controller.Conflict(localizer["DuplicateVehicleCode"].Value),

                    CreateVehicleError.DuplicateLicensePlate =>
                        controller.Conflict(localizer["DuplicateLicensePlate"].Value),

                    CreateVehicleError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingVehicle"].Value,
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
