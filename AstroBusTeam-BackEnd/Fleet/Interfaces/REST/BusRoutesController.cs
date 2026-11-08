using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST;

/// <summary>
///     REST controller for routes
/// </summary>
[ApiController]
[Route("api/v1/routes")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("BusRoutes")]
public class BusRoutesController(
    IBusRouteCommandService commandService,
    IBusRouteQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<BusRoutesController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a route.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BusRouteResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateBusRoute([FromBody] CreateBusRouteResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateBusRouteCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateBusRouteResultAssembler.ToActionResultFromCreateBusRouteResult(
                result,
                this,
                localizer,
                nameof(GetBusRouteById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating route");
            return BadRequest(localizer["InvalidBusRouteRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating route");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingBusRoute"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists routes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BusRouteResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetBusRoutesFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllBusRoutesQuery();
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(BusRouteResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying routes");
            return BadRequest(localizer["InvalidBusRouteQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a route by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BusRouteResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetBusRouteById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetBusRouteByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(BusRouteResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a route.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BusRouteResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateBusRoute(int id, [FromBody] UpdateBusRouteResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateBusRouteCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateBusRouteResultAssembler.ToActionResultFromUpdateBusRouteResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating route {Id}", id);
            return BadRequest(localizer["InvalidBusRouteRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating route {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingBusRoute"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a route.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteBusRoute(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteBusRouteCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteBusRouteResultAssembler.ToActionResultFromDeleteBusRouteResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting route {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingBusRoute"].Value,
                statusCode: 500);
        }
    }
}
