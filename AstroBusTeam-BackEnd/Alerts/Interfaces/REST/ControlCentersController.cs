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
///     REST controller for control centers
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("ControlCenters")]
public class ControlCentersController(
    IControlCenterCommandService commandService,
    IControlCenterQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<ControlCentersController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a control center.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ControlCenterResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateControlCenter([FromBody] CreateControlCenterResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateControlCenterCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateControlCenterResultAssembler.ToActionResultFromCreateControlCenterResult(
                result,
                this,
                localizer,
                nameof(GetControlCenterById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating control center");
            return BadRequest(localizer["InvalidControlCenterRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating control center");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingControlCenter"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists control centers.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ControlCenterResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetControlCentersFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllControlCentersQuery();
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(ControlCenterResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying control centers");
            return BadRequest(localizer["InvalidControlCenterQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a control center by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ControlCenterResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetControlCenterById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetControlCenterByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(ControlCenterResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a control center.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ControlCenterResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateControlCenter(int id, [FromBody] UpdateControlCenterResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateControlCenterCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateControlCenterResultAssembler.ToActionResultFromUpdateControlCenterResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating control center {Id}", id);
            return BadRequest(localizer["InvalidControlCenterRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating control center {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingControlCenter"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a control center.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteControlCenter(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteControlCenterCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteControlCenterResultAssembler.ToActionResultFromDeleteControlCenterResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting control center {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingControlCenter"].Value,
                statusCode: 500);
        }
    }
}
