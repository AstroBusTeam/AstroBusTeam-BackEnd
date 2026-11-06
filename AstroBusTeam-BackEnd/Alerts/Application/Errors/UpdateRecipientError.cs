namespace SecurityBus.API.Alerts.Application.Errors;

/// <summary>
/// Represents errors that can occur when updating a recipient.
/// </summary>
public enum UpdateRecipientError
{
	/// <summary>
	/// The recipient was not found.
	/// </summary>
	RecipientNotFound,

	/// <summary>
	/// The recipient data contains invalid values.
	/// </summary>
	InvalidData,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
