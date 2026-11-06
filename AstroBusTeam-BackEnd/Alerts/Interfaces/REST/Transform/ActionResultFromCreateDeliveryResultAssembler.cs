using SecurityBus.API.Alerts.Application.Errors;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Resources;
using SecurityBus.API.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to create a delivery in the application layer.
/// </summary>
public static class ActionResultFromCreateDeliveryResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a create delivery operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <param name="getByIdActionName">The name of the action used to generate the location header.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromCreateDeliveryResult(
        Result<Delivery, CreateDeliveryError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getByIdActionName) =>
        result switch
        {
            Result<Delivery, CreateDeliveryError>.Success success =>
                controller.CreatedAtAction(getByIdActionName, new { id = success.Value.Id },
                    DeliveryResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Delivery, CreateDeliveryError>.Failure failure =>
                failure.Error switch
                {
                    CreateDeliveryError.InvalidData =>
                        controller.BadRequest(localizer["InvalidDeliveryData"].Value),

                    CreateDeliveryError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingDelivery"].Value,
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
