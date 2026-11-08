namespace AstroBusTeamBackEnd.Iam.Application.Errors;

/// <summary>
///     Errors that can occur in the DeleteUserError operation
/// </summary>
public enum DeleteUserError
{
	UserNotFound,

	UserHasRelatedData,

	UnexpectedError
}
