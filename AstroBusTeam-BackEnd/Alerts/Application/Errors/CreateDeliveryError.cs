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
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
