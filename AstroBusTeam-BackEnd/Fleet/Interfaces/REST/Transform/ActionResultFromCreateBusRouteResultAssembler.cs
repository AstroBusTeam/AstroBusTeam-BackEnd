using AstroBusTeamBackEnd.Fleet.Application.Errors;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts the result of the create route operation into an HTTP response
/// </summary>
public static class ActionResultFromCreateBusRouteResultAssembler
{
    /// <summary>
    ///     Converts the result into an action result.
    /// </summary>
    public static ActionResult ToActionResultFromCreateBusRouteResult(
        Result<BusRoute, CreateBusRouteError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<BusRoute, CreateBusRouteError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    BusRouteResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<BusRoute, CreateBusRouteError>.Failure failure =>
                failure.Error switch
                {
                    CreateBusRouteError.InvalidData =>
                        controller.BadRequest(localizer["InvalidBusRouteData"].Value),
                    CreateBusRouteError.DuplicateCode =>
                        controller.Conflict(localizer["DuplicateBusRouteCode"].Value),
                    CreateBusRouteError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingBusRoute"].Value,
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
