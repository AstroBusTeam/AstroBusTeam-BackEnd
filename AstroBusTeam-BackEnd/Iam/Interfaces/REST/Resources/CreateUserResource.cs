using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a user
/// </summary>
public record CreateUserResource(
    [Required(ErrorMessage = "RequiredUserUsername")]
    string Username,
    [Required(ErrorMessage = "RequiredUserPassword")]
    string Password,
    string? Role = null,
    int? DriverId = null,
    bool? Active = null);
