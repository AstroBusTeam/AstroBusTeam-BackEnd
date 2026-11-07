using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to update a driver in the application layer.
/// </summary>
public static class ActionResultFromUpdateDriverResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a update driver operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromUpdateDriverResult(
        Result<Driver, UpdateDriverError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Driver, UpdateDriverError>.Success success =>
                controller.Ok(DriverResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Driver, UpdateDriverError>.Failure failure =>
                failure.Error switch
                {
                    UpdateDriverError.DriverNotFound =>
                        controller.NotFound(localizer["DriverNotFound"].Value),

                    UpdateDriverError.InvalidData =>
                        controller.BadRequest(localizer["InvalidDriverData"].Value),

                    UpdateDriverError.DuplicateEmployeeCode =>
                        controller.Conflict(localizer["DuplicateEmployeeCode"].Value),

                    UpdateDriverError.DuplicateDni =>
                        controller.Conflict(localizer["DuplicateDni"].Value),

                    UpdateDriverError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingDriver"].Value,
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
