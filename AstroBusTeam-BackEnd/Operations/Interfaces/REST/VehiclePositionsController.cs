using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using AstroBusTeamBackEnd.Operations.Application.Services;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.Queries;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Operations.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Operations.Interfaces.REST;

/// <summary>
///     REST controller for vehicle positions
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("VehiclePositions")]
public class VehiclePositionsController(
    IVehiclePositionCommandService commandService,
    IVehiclePositionQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<VehiclePositionsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a vehicle position.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VehiclePositionResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateVehiclePosition([FromBody] CreateVehiclePositionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateVehiclePositionCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateVehiclePositionResultAssembler.ToActionResultFromCreateVehiclePositionResult(
                result,
                this,
                localizer,
                nameof(GetVehiclePositionById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating vehicle position");
            return BadRequest(localizer["InvalidVehiclePositionRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating vehicle position");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingVehiclePosition"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists vehicle positions.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehiclePositionResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetVehiclePositionsFromQuery(
        [FromQuery] int? vehicleId = null,
        [FromQuery] int? shiftId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllVehiclePositionsQuery(vehicleId, shiftId);
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(VehiclePositionResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying vehicle positions");
            return BadRequest(localizer["InvalidVehiclePositionQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a vehicle position by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehiclePositionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetVehiclePositionById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetVehiclePositionByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(VehiclePositionResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a vehicle position.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VehiclePositionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateVehiclePosition(int id, [FromBody] UpdateVehiclePositionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateVehiclePositionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateVehiclePositionResultAssembler.ToActionResultFromUpdateVehiclePositionResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating vehicle position {Id}", id);
            return BadRequest(localizer["InvalidVehiclePositionRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating vehicle position {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingVehiclePosition"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a vehicle position.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteVehiclePosition(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteVehiclePositionCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteVehiclePositionResultAssembler.ToActionResultFromDeleteVehiclePositionResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting vehicle position {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingVehiclePosition"].Value,
                statusCode: 500);
        }
    }
}
