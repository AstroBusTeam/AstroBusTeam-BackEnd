using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a delivery
/// </summary>
public record CreateDeliveryCommand(int AlertId, int RecipientId, int ChannelId, int AlertTypeId, string Priority, DeliveryStatus Status, DateTime SentAt, string? ErrorMessage);
