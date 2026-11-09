using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using AstroBusTeamBackEnd.Iam.Application.Services;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Domain.Model.Queries;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;
using AstroBusTeamBackEnd.Iam.Interfaces.REST.Transform;
using AstroBusTeamBackEnd.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST;

/// <summary>
///     REST controller for users
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Users")]
public class UsersController(
    IUserCommandService commandService,
    IUserQueryService queryService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<UsersController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromCreateUserResultAssembler.ToActionResultFromCreateUserResult(
                result,
                this,
                localizer,
                nameof(GetUserById));
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while creating user");
            return BadRequest(localizer["InvalidUserRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating user");
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingUser"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Lists users.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetUsersFromQuery(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllUsersQuery();
            var result = await queryService.Handle(query, cancellationToken);
            return Ok(result.Select(UserResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while querying users");
            return BadRequest(localizer["InvalidUserQuery"].Value);
        }
    }

    /// <summary>
    ///     Gets a user by identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdQuery(id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result is null) return NotFound();
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    ///     Updates a user.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromUpdateUserResultAssembler.ToActionResultFromUpdateUserResult(result, this, localizer);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException)
        {
            logger.LogWarning(ex, "Validation failed while updating user {Id}", id);
            return BadRequest(localizer["InvalidUserRequest"].Value);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while updating user {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorUpdatingUser"].Value,
                statusCode: 500);
        }
    }

    /// <summary>
    ///     Deletes a user.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteUserCommand(id);
            var result = await commandService.Handle(command, cancellationToken);
            return ActionResultFromDeleteUserResultAssembler.ToActionResultFromDeleteUserResult(result, this, localizer);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting user {Id}", id);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorDeletingUser"].Value,
                statusCode: 500);
        }
    }
}
