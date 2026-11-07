using System.Net.Mime;
using SecurityBus.API.Operations.Application.Services;
using SecurityBus.API.Operations.Domain.Model.Queries;
using SecurityBus.API.Operations.Domain.Model.ValueObjects;
using SecurityBus.API.Operations.Domain.Model.Commands;
using SecurityBus.API.Operations.Interfaces.REST.Resources;
using SecurityBus.API.Operations.Interfaces.REST.Transform;
using SecurityBus.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Operations.Interfaces.REST;

/// <summary>
///     Shifts' controller.
/// </summary>
/// <param name="shiftCommandService">The Shift Command Service</param>
/// <param name="shiftQueryService">The Shift Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Shifts")]
public class ShiftsController(
    IShiftCommandService shiftCommandService,
    IShiftQueryService shiftQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<ShiftsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a shift.
    /// </summary>
    /// <param name="resource">CreateShiftResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created shift, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(ShiftResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateShift([FromBody] CreateShiftResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateShiftCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await shiftCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateShiftResultAssembler.ToActionResultFromCreateShiftResult(
                result,
                this,
                localizer,
                nameof(GetShiftById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating shift");
            return BadRequest(localizer["InvalidShiftRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating shift");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingShift"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all shifts, optionally filtered.
    /// </summary>
    /// <param name="driverId">Optional filter by driver id</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the shifts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ShiftResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetShiftsFromQuery(
        [FromQuery] int? driverId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllShiftsQuery(driverId);
            var result = await shiftQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(ShiftResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying shifts");
            return BadRequest(localizer["InvalidShiftQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a shift by id.
    /// </summary>
    /// <param name="id">The shift ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the shift, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ShiftResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetShiftById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetShiftByIdQuery(id);
        var result = await shiftQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(ShiftResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a shift.
    /// </summary>
    /// <param name="id">The shift ID</param>
    /// <param name="resource">UpdateShiftResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the updated shift, or an error response if it was not updated.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ShiftResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateShift(int id, [FromBody] UpdateShiftResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateShiftCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await shiftCommandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateShiftResultAssembler.ToActionResultFromUpdateShiftResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating shift {Id}", id);
            return BadRequest(localizer["InvalidShiftRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating shift {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingShift"].Value,
                statusCode: 500);
        }
    }
}
