namespace AstroBusTeamBackEnd.Operations.Domain.Model.Queries;

/// <summary>
///     Query to list vehicle positions
/// </summary>
public record GetAllVehiclePositionsQuery(int? VehicleId = null, int? ShiftId = null);
