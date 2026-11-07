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
///     Assignments' controller.
/// </summary>
/// <param name="assignmentCommandService">The Assignment Command Service</param>
/// <param name="assignmentQueryService">The Assignment Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Assignments")]
public class AssignmentsController(
    IAssignmentCommandService assignmentCommandService,
    IAssignmentQueryService assignmentQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<AssignmentsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a assignment.
    /// </summary>
    /// <param name="resource">CreateAssignmentResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created assignment, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(AssignmentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAssignment([FromBody] CreateAssignmentResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateAssignmentCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await assignmentCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateAssignmentResultAssembler.ToActionResultFromCreateAssignmentResult(
                result,
                this,
                localizer,
                nameof(GetAssignmentById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating assignment");
            return BadRequest(localizer["InvalidAssignmentRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating assignment");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingAssignment"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all assignments, optionally filtered.
    /// </summary>
    /// <param name="driverId">Optional filter by driver id</param>
    /// <param name="status">Optional filter by status</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the assignments.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AssignmentResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAssignmentsFromQuery(
        [FromQuery] int? driverId = null,
        [FromQuery] string? status = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllAssignmentsQuery(driverId, status is null ? null : new AssignmentStatus(status));
            var result = await assignmentQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(AssignmentResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying assignments");
            return BadRequest(localizer["InvalidAssignmentQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a assignment by id.
    /// </summary>
    /// <param name="id">The assignment ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the assignment, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssignmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAssignmentByIdQuery(id);
        var result = await assignmentQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(AssignmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Deletes a assignment.
    /// </summary>
    /// <param name="id">The assignment ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An empty JSON object when the assignment was deleted, or an error response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAssignment(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAssignmentCommand(id);
            var result = await assignmentCommandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteAssignmentResultAssembler.ToActionResultFromDeleteAssignmentResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting assignment {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingAssignment"].Value,
                statusCode: 500);
        }
    }
}
