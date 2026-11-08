namespace AstroBusTeamBackEnd.Iam.Application.Errors;

/// <summary>
///     Errors that can occur in the CreateUserError operation
/// </summary>
public enum CreateUserError
{
	InvalidData,

	DuplicateUsername,

	InvalidReference,

	UnexpectedError
}
