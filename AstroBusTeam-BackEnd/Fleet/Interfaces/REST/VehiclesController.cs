using System.Net.Mime;
using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Queries;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Fleet.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST;

/// <summary>
///     Vehicles' controller.
/// </summary>
/// <param name="vehicleCommandService">The Vehicle Command Service</param>
/// <param name="vehicleQueryService">The Vehicle Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Vehicles")]
public class VehiclesController(
    IVehicleCommandService vehicleCommandService,
    IVehicleQueryService vehicleQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<VehiclesController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a vehicle.
    /// </summary>
    /// <param name="resource">CreateVehicleResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created vehicle, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateVehicle([FromBody] CreateVehicleResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await vehicleCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateVehicleResultAssembler.ToActionResultFromCreateVehicleResult(
                result,
                this,
                localizer,
                nameof(GetVehicleById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating vehicle");
            return BadRequest(localizer["InvalidVehicleRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating vehicle");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingVehicle"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all vehicles, optionally filtered.
    /// </summary>

    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the vehicles.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetVehiclesFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllVehiclesQuery();
            var result = await vehicleQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying vehicles");
            return BadRequest(localizer["InvalidVehicleQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a vehicle by id.
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the vehicle, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetVehicleById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetVehicleByIdQuery(id);
        var result = await vehicleQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(VehicleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a vehicle.
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <param name="resource">UpdateVehicleResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the updated vehicle, or an error response if it was not updated.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VehicleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateVehicleCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await vehicleCommandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateVehicleResultAssembler.ToActionResultFromUpdateVehicleResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating vehicle {Id}", id);
            return BadRequest(localizer["InvalidVehicleRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating vehicle {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingVehicle"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a vehicle.
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An empty JSON object when the vehicle was deleted, or an error response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteVehicle(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteVehicleCommand(id);
            var result = await vehicleCommandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteVehicleResultAssembler.ToActionResultFromDeleteVehicleResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting vehicle {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingVehicle"].Value,
                statusCode: 500);
        }
    }
}
