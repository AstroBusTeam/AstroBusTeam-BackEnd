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
///     REST controller for alert types
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("AlertTypes")]
public class AlertTypesController(
    IAlertTypeCommandService commandService,
    IAlertTypeQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<AlertTypesController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a alert type.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AlertTypeResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAlertType([FromBody] CreateAlertTypeResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateAlertTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateAlertTypeResultAssembler.ToActionResultFromCreateAlertTypeResult(
                result,
                this,
                localizer,
                nameof(GetAlertTypeById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating alert type");
            return BadRequest(localizer["InvalidAlertTypeRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating alert type");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingAlertType"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists alert types.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlertTypeResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAlertTypesFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllAlertTypesQuery();
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(AlertTypeResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying alert types");
            return BadRequest(localizer["InvalidAlertTypeQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a alert type by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlertTypeResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAlertTypeById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAlertTypeByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(AlertTypeResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a alert type.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AlertTypeResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAlertType(int id, [FromBody] UpdateAlertTypeResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateAlertTypeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateAlertTypeResultAssembler.ToActionResultFromUpdateAlertTypeResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating alert type {Id}", id);
            return BadRequest(localizer["InvalidAlertTypeRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating alert type {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingAlertType"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a alert type.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAlertType(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAlertTypeCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteAlertTypeResultAssembler.ToActionResultFromDeleteAlertTypeResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting alert type {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingAlertType"].Value,
                statusCode: 500);
        }
    }
}
