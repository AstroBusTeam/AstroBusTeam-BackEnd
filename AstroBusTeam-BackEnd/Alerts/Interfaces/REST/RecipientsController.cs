using System.Net.Mime;
using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST;

/// <summary>
///     Recipients' controller.
/// </summary>
/// <param name="recipientCommandService">The Recipient Command Service</param>
/// <param name="recipientQueryService">The Recipient Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Recipients")]
public class RecipientsController(
    IRecipientCommandService recipientCommandService,
    IRecipientQueryService recipientQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<RecipientsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Gets all recipients, optionally filtered.
    /// </summary>

    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the recipients.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipientResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetRecipientsFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllRecipientsQuery();
            var result = await recipientQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(RecipientResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying recipients");
            return BadRequest(localizer["InvalidRecipientQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a recipient by id.
    /// </summary>
    /// <param name="id">The recipient ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the recipient, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RecipientResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetRecipientById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetRecipientByIdQuery(id);
        var result = await recipientQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(RecipientResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a recipient.
    /// </summary>
    /// <param name="id">The recipient ID</param>
    /// <param name="resource">UpdateRecipientResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the updated recipient, or an error response if it was not updated.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RecipientResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRecipient(int id, [FromBody] UpdateRecipientResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateRecipientCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await recipientCommandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateRecipientResultAssembler.ToActionResultFromUpdateRecipientResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating recipient {Id}", id);
            return BadRequest(localizer["InvalidRecipientRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating recipient {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingRecipient"].Value,
                statusCode: 500);
        }
    }
}
