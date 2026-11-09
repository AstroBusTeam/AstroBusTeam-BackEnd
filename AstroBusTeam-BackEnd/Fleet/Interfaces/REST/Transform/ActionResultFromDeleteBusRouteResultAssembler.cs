using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the delete route operation into an HTTP response
/// </summary>
public static class ActionResultFromDeleteBusRouteResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromDeleteBusRouteResult(
        Result<BusRoute, DeleteBusRouteError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<BusRoute, DeleteBusRouteError>.Success success =>
                controller.Ok(new object()),
            Result<BusRoute, DeleteBusRouteError>.Failure failure =>
                failure.Error switch
                {
                    DeleteBusRouteError.BusRouteNotFound =>
                        controller.NotFound(localizer["BusRouteNotFound"].Value),
                    DeleteBusRouteError.BusRouteHasRelatedData =>
                        controller.Conflict(localizer["BusRouteHasRelatedData"].Value),
                    DeleteBusRouteError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorDeletingBusRoute"].Value,
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
