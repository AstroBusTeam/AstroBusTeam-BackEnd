using SecurityBus.API.Alerts.Application.Services;
using SecurityBus.API.Alerts.Domain.Model.Aggregates;
using SecurityBus.API.Alerts.Domain.Model.Queries;
using SecurityBus.API.Alerts.Domain.Repositories;

namespace SecurityBus.API.Alerts.Application.Internal.QueryServices;

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
        await recipientRepository.FindByIdAsync(query.Id, cancellationToken);
}
