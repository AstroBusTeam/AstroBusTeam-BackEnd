using SecurityBus.API.Operations.Application.Errors;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to create a shift in the application layer.
/// </summary>
public static class ActionResultFromCreateShiftResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a create shift operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <param name="getByIdActionName">The name of the action used to generate the location header.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromCreateShiftResult(
        Result<Shift, CreateShiftError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<Shift, CreateShiftError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    ShiftResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Shift, CreateShiftError>.Failure failure =>
                failure.Error switch
                {
                    CreateShiftError.InvalidData =>
                        controller.BadRequest(localizer["InvalidShiftData"].Value),

                    CreateShiftError.DriverNotFound =>
                        controller.BadRequest(localizer["ShiftDriverNotFound"].Value),

                    CreateShiftError.VehicleNotFound =>
                        controller.BadRequest(localizer["ShiftVehicleNotFound"].Value),

                    CreateShiftError.ActiveShiftAlreadyExists =>
                        controller.Conflict(localizer["ActiveShiftAlreadyExists"].Value),

                    CreateShiftError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingShift"].Value,
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
