using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Iam.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a user
/// </summary>
public record UpdateUserResource(
    [Required(ErrorMessage = "RequiredUserUsername")]
    string Username,
    [Required(ErrorMessage = "RequiredUserRole")]
    string Role,
    bool Active,
    string? Password = null,
    int? DriverId = null);
