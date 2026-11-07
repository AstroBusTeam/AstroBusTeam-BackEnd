namespace AstroBusTeamBackEnd.Fleet.Application.Errors;

/// <summary>
/// Represents errors that can occur when deleting a assignment.
/// </summary>
public enum DeleteAssignmentError
{
	/// <summary>
	/// The assignment was not found.
	/// </summary>
	AssignmentNotFound,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
