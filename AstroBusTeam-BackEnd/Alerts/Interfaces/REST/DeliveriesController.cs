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
///     Deliveries' controller.
/// </summary>
/// <param name="deliveryCommandService">The Delivery Command Service</param>
/// <param name="deliveryQueryService">The Delivery Query Service</param>
/// <param name="localizer">String localizer for response messages</param>
/// <param name="logger">Logger for diagnostics and error reporting</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Deliveries")]
public class DeliveriesController(
    IDeliveryCommandService deliveryCommandService,
    IDeliveryQueryService deliveryQueryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<DeliveriesController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a delivery.
    /// </summary>
    /// <param name="resource">CreateDeliveryResource resource</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the created delivery, or an error response if it was not created.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(DeliveryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateDelivery([FromBody] CreateDeliveryResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateDeliveryCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await deliveryCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateDeliveryResultAssembler.ToActionResultFromCreateDeliveryResult(
                result,
                this,
                localizer,
                nameof(GetDeliveryById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating delivery");
            return BadRequest(localizer["InvalidDeliveryRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating delivery");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingDelivery"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Gets all deliveries, optionally filtered.
    /// </summary>

    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A response as an action result containing the deliveries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetDeliveriesFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllDeliveriesQuery();
            var result = await deliveryQueryService.Handle(query, cancellationToken);
            return Ok(result.Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying deliveries");
            return BadRequest(localizer["InvalidDeliveryQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a delivery by id.
    /// </summary>
    /// <param name="id">The delivery ID</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A response as an action result containing the delivery, or not found if it was not found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DeliveryResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetDeliveryById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetDeliveryByIdQuery(id);
        var result = await deliveryQueryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
