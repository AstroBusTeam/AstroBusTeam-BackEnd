using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST;

/// <summary>
///     REST controller for alert attempts
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("AlertAttempts")]
public class AlertAttemptsController(
    IAlertAttemptCommandService commandService,
    IAlertAttemptQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<AlertAttemptsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a alert attempt.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AlertAttemptResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAlertAttempt([FromBody] CreateAlertAttemptResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateAlertAttemptCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateAlertAttemptResultAssembler.ToActionResultFromCreateAlertAttemptResult(
                result,
                this,
                localizer,
                nameof(GetAlertAttemptById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating alert attempt");
            return BadRequest(localizer["InvalidAlertAttemptRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert attempt");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingAlertAttempt"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists alert attempts.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlertAttemptResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAlertAttemptsFromQuery(
        [FromQuery] int? alertId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllAlertAttemptsQuery(alertId);
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(AlertAttemptResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying alert attempts");
            return BadRequest(localizer["InvalidAlertAttemptQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a alert attempt by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlertAttemptResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAlertAttemptById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAlertAttemptByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(AlertAttemptResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a alert attempt.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AlertAttemptResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAlertAttempt(int id, [FromBody] UpdateAlertAttemptResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateAlertAttemptCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateAlertAttemptResultAssembler.ToActionResultFromUpdateAlertAttemptResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating alert attempt {Id}", id);
            return BadRequest(localizer["InvalidAlertAttemptRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert attempt {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingAlertAttempt"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a alert attempt.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAlertAttempt(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAlertAttemptCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteAlertAttemptResultAssembler.ToActionResultFromDeleteAlertAttemptResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert attempt {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingAlertAttempt"].Value,
                statusCode: 500);
        }
    }
}
