using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Queries;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;

namespace AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;

/// <summary>
///     Application service for querying recipients.
/// </summary>
/// <remarks>
///     This service acts as the query handler for retrieving recipients. All queries delegate to the repository layer.
/// </remarks>
/// <param name="recipientRepository">Repository for accessing recipient data.</param>
public class RecipientQueryService(IRecipientRepository recipientRepository) : IRecipientQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Recipient>> Handle(GetAllRecipientsQuery query, CancellationToken cancellationToken = default) =>
        await recipientRepository.FindAllAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Recipient?> Handle(GetRecipientByIdQuery query, CancellationToken cancellationToken = default) =>
        await recipientRepository.FindByIdWithChannelsAsync(query.Id, cancellationToken);
}
