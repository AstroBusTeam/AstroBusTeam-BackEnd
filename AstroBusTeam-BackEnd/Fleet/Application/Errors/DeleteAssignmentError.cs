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
	/// The assignment is referenced by other records (shifts) and cannot be deleted.
	/// </summary>
	AssignmentHasRelatedData,

	/// <summary>
	/// An unexpected error occurred during the operation.
	/// </summary>
	UnexpectedError
}
