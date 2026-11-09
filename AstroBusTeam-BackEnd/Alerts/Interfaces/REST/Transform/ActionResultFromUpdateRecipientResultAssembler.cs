using AstroBusTeamBackEnd.Alerts.Application.Errors;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembles an HTTP action result from the result of the command handling to update a recipient in the application layer.
/// </summary>
public static class ActionResultFromUpdateRecipientResultAssembler
{
    /// <summary>
    ///     Assembles an <see cref="ActionResult" /> from the result of a update recipient operation.
    /// </summary>
    /// <param name="result">The application result produced by the application layer after handling the command.</param>
    /// <param name="controller">The controller used to produce HTTP-specific action results.</param>
    /// <param name="localizer">The string localizer used for API response messages.</param>
    /// <returns>The HTTP action result corresponding to the application outcome.</returns>
    public static ActionResult ToActionResultFromUpdateRecipientResult(
        Result<Recipient, UpdateRecipientError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<Recipient, UpdateRecipientError>.Success success =>
                controller.Ok(RecipientResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<Recipient, UpdateRecipientError>.Failure failure =>
                failure.Error switch
                {
                    UpdateRecipientError.RecipientNotFound =>
                        controller.NotFound(localizer["RecipientNotFound"].Value),

                    UpdateRecipientError.InvalidData =>
                        controller.BadRequest(localizer["InvalidRecipientData"].Value),

                    UpdateRecipientError.InvalidReference =>
                        controller.BadRequest(localizer["InvalidRecipientReference"].Value),
                    UpdateRecipientError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorUpdatingRecipient"].Value,
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
