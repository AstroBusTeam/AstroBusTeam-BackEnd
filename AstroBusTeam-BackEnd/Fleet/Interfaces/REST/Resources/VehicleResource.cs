namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a vehicle.
/// </summary>
/// <param name="Id">The server-generated ID of the vehicle</param>
/// <param name="Code">Unit code</param>
/// <param name="Plate">License plate</param>
/// <param name="Model">Model</param>
/// <param name="Capacity">Passenger capacity</param>
/// <param name="Status">Vehicle status (available, in-service or maintenance)</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record VehicleResource(
    int Id,
    string Code,
    string Plate,
    string Model,
    int Capacity,
    string Status,
    double Latitude,
    double Longitude);
