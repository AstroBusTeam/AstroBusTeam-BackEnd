using System.Net.Mime;
using SecurityBus.API.Alerts.Application.Services;
using SecurityBus.API.Alerts.Domain.Model.Queries;
using SecurityBus.API.Alerts.Domain.Model.ValueObjects;
using SecurityBus.API.Alerts.Domain.Model.Commands;
using SecurityBus.API.Alerts.Interfaces.REST.Resources;
using SecurityBus.API.Alerts.Interfaces.REST.Transform;
using SecurityBus.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Alerts.Interfaces.REST;

/// <summary>
///     Alerts' controller.
/// </summary>
/// <param name="alertCommandService">The Alert Command Service</param>
/// <param name="alertQueryService">The Alert Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Alerts")]
public class AlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<AlertsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a alert.
    /// </summary>
    /// <param name="resource">CreateAlertResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created alert, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAlert([FromBody] CreateAlertResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await alertCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateAlertResultAssembler.ToActionResultFromCreateAlertResult(
                result,
                this,
                localizer,
                nameof(GetAlertById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating alert");
            return BadRequest(localizer["InvalidAlertRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingAlert"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all alerts, optionally filtered.
    /// </summary>
    /// <param name="status">Optional filter by status</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the alerts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlertResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAlertsFromQuery(
        [FromQuery] string? status = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllAlertsQuery(status is null ? null : new AlertStatus(status));
            var result = await alertQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying alerts");
            return BadRequest(localizer["InvalidAlertQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a alert by id.
    /// </summary>
    /// <param name="id">The alert ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the alert, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAlertById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAlertByIdQuery(id);
        var result = await alertQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a alert.
    /// </summary>
    /// <param name="id">The alert ID</param>
    /// <param name="resource">UpdateAlertResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the updated alert, or an error response if it was not updated.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAlert(int id, [FromBody] UpdateAlertResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateAlertCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await alertCommandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateAlertResultAssembler.ToActionResultFromUpdateAlertResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating alert {Id}", id);
            return BadRequest(localizer["InvalidAlertRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingAlert"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a alert.
    /// </summary>
    /// <param name="id">The alert ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An empty JSON object when the alert was deleted, or an error response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAlert(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAlertCommand(id);
            var result = await alertCommandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteAlertResultAssembler.ToActionResultFromDeleteAlertResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingAlert"].Value,
                statusCode: 500);
        }
    }
}
