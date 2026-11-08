using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the update route operation into an HTTP response
/// </summary>
public static class ActionResultFromUpdateBusRouteResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromUpdateBusRouteResult(
        Result<BusRoute, UpdateBusRouteError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<BusRoute, UpdateBusRouteError>.Success success =>
                controller.Ok(BusRouteResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<BusRoute, UpdateBusRouteError>.Failure failure =>
                failure.Error switch
                {
                    UpdateBusRouteError.BusRouteNotFound =>
                        controller.NotFound(localizer["BusRouteNotFound"].Value),
                    UpdateBusRouteError.InvalidData =>
                        controller.BadRequest(localizer["InvalidBusRouteData"].Value),
                    UpdateBusRouteError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateBusRouteCode"].Value),
                    UpdateBusRouteError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingBusRoute"].Value,
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
