using SecurityBus.API.Operations.Application.Errors;
using SecurityBus.API.Operations.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Operations.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to update a shift in the application layer.
/// </summary>
public static class ActionResultFromUpdateShiftResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a update shift operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromUpdateShiftResult(
        Result<Shift, UpdateShiftError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Shift, UpdateShiftError>.Success success =>
                controller.Ok(ShiftResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Shift, UpdateShiftError>.Failure failure =>
                failure.Error switch
                {
                    UpdateShiftError.ShiftNotFound =>
                        controller.NotFound(localizer["ShiftNotFound"].Value),

                    UpdateShiftError.InvalidData =>
                        controller.BadRequest(localizer["InvalidShiftData"].Value),

                    UpdateShiftError.InvalidShiftStateTransition =>
                        controller.Conflict(localizer["InvalidShiftStateTransition"].Value),

                    UpdateShiftError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingShift"].Value,
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
