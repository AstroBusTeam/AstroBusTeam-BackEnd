using System.Net.Mime;
using SecurityBus.API.Fleet.Application.Services;
using SecurityBus.API.Fleet.Domain.Model.Queries;
using SecurityBus.API.Fleet.Domain.Model.ValueObjects;
using SecurityBus.API.Fleet.Domain.Model.Commands;
using SecurityBus.API.Fleet.Interfaces.REST.Resources;
using SecurityBus.API.Fleet.Interfaces.REST.Transform;
using SecurityBus.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SecurityBus.API.Fleet.Interfaces.REST;

/// <summary>
///     Drivers' controller.
/// </summary>
/// <param name="driverCommandService">The Driver Command Service</param>
/// <param name="driverQueryService">The Driver Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Drivers")]
public class DriversController(
    IDriverCommandService driverCommandService,
    IDriverQueryService driverQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<DriversController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a driver.
    /// </summary>
    /// <param name="resource">CreateDriverResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created driver, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(DriverResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateDriver([FromBody] CreateDriverResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateDriverCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await driverCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateDriverResultAssembler.ToActionResultFromCreateDriverResult(
                result,
                this,
                localizer,
                nameof(GetDriverById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating driver");
            return BadRequest(localizer["InvalidDriverRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating driver");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingDriver"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all drivers, optionally filtered.
    /// </summary>
    /// <param name="employeeCode">Optional filter by employee code</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the drivers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetDriversFromQuery(
        [FromQuery] string? employeeCode = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllDriversQuery(employeeCode is null ? null : new EmployeeCode(employeeCode));
            var result = await driverQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(DriverResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying drivers");
            return BadRequest(localizer["InvalidDriverQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a driver by id.
    /// </summary>
    /// <param name="id">The driver ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the driver, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DriverResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetDriverById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetDriverByIdQuery(id);
        var result = await driverQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(DriverResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a driver.
    /// </summary>
    /// <param name="id">The driver ID</param>
    /// <param name="resource">UpdateDriverResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the updated driver, or an error response if it was not updated.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DriverResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateDriver(int id, [FromBody] UpdateDriverResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateDriverCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await driverCommandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateDriverResultAssembler.ToActionResultFromUpdateDriverResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating driver {Id}", id);
            return BadRequest(localizer["InvalidDriverRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating driver {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingDriver"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a driver.
    /// </summary>
    /// <param name="id">The driver ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An empty JSON object when the driver was deleted, or an error response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteDriver(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteDriverCommand(id);
            var result = await driverCommandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteDriverResultAssembler.ToActionResultFromDeleteDriverResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting driver {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingDriver"].Value,
                statusCode: 500);
        }
    }
}
