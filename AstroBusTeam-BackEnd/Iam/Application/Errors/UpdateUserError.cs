namespace AstroBusTeamBackEnd.Iam.Application.Errors;

/// <summary>
///     Errors that can occur in the UpdateUserError operation
/// </summary>
public enum UpdateUserError
{
	UserNotFound,

	InvalidData,

	DuplicateUsername,

	InvalidReference,

	UnexpectedError
}
