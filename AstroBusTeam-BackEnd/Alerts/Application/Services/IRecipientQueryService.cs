using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;

namespace AstroBusTeamBackEnd.Alerts.Application.Services;

/// <summary>
///     Query service interface for recipient operations.
/// </summary>
/// <remarks>
///     This interface defines the contract for querying recipients. It supports listing (with optional filters) and
///     retrieving a single recipient by its identifier.
/// </remarks>
public interface IRecipientQueryService
{
    /// <summary>
    ///     Handle the GetAllRecipientsQuery.
    /// </summary>
    /// <param name="query">The GetAllRecipientsQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable containing the Recipient objects</returns>
    Task<IEnumerable<Recipient>> Handle(GetAllRecipientsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handle the GetRecipientByIdQuery.
    /// </summary>
    /// <param name="query">The GetRecipientByIdQuery query</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The Recipient object if found, or null otherwise</returns>
    Task<Recipient?> Handle(GetRecipientByIdQuery query, CancellationToken cancellationToken = default);
}
