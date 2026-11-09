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
///     REST controller for notification channels
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("NotificationChannels")]
public class NotificationChannelsController(
    INotificationChannelCommandService commandService,
    INotificationChannelQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<NotificationChannelsController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a notification channel.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(NotificationChannelResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateNotificationChannel([FromBody] CreateNotificationChannelResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateNotificationChannelCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateNotificationChannelResultAssembler.ToActionResultFromCreateNotificationChannelResult(
                result,
                this,
                localizer,
                nameof(GetNotificationChannelById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating notification channel");
            return BadRequest(localizer["InvalidNotificationChannelRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating notification channel");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingNotificationChannel"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists notification channels.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotificationChannelResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetNotificationChannelsFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllNotificationChannelsQuery();
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(NotificationChannelResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying notification channels");
            return BadRequest(localizer["InvalidNotificationChannelQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a notification channel by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NotificationChannelResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetNotificationChannelById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetNotificationChannelByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(NotificationChannelResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a notification channel.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NotificationChannelResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateNotificationChannel(int id, [FromBody] UpdateNotificationChannelResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateNotificationChannelCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateNotificationChannelResultAssembler.ToActionResultFromUpdateNotificationChannelResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating notification channel {Id}", id);
            return BadRequest(localizer["InvalidNotificationChannelRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating notification channel {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingNotificationChannel"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a notification channel.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteNotificationChannel(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteNotificationChannelCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteNotificationChannelResultAssembler.ToActionResultFromDeleteNotificationChannelResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting notification channel {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingNotificationChannel"].Value,
                statusCode: 500);
        }
    }
}
