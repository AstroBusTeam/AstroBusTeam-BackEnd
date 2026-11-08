namespace AstroBusTeamBackEnd.Alerts.Application.Errors;

/// <summary>
/// Represents errors that can occur when creating a delivery.
/// </summary>
public enum CreateDeliveryError
{
	/// <summary>
	/// The delivery data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// A referenced record (alert, recipient, channel or alert type) does not exist.
	/// </summary>
	InvalidReference,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
