using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Sample data taken from the Fake API (db.json) so the real API can be used by the existing frontend right away.
/// </summary>
/// <remarks>
///     Every aggregate is created through its own creation command, so the sample data goes through the same domain
///     validation as any request.
/// </remarks>
public static class SampleData
{
    /// <summary>
    ///     Gets the sample drivers of the Fake API.
    /// </summary>
    public static IReadOnlyList<Driver> Drivers() =>
    [
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-90210"),
                "Marcos",
                "Silva",
                new Dni("77443322"),
                "A-IIIb",
                new LicenseNumber("Q77443322"),
                new DateOnly(2027, 5, 12),
                14,
                4.9m,
                12,
                new DriverStatus("active"))),
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-88294"),
                "Alejandro",
                "Ortiz",
                new Dni("11223344"),
                "A-IIIb",
                new LicenseNumber("Q11223344"),
                new DateOnly(2026, 12, 5),
                15,
                4.8m,
                8,
                new DriverStatus("active"))),
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-85401"),
                "Elena",
                "Garrido",
                new Dni("45678912"),
                "A-IIIc",
                new LicenseNumber("Q45678912"),
                new DateOnly(2028, 3, 20),
                12,
                4.6m,
                5,
                new DriverStatus("active"))),
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-87910"),
                "Roberto",
                "Valdez",
                new Dni("99001122"),
                "A-IIIb",
                new LicenseNumber("Q99001122"),
                new DateOnly(2026, 8, 30),
                10,
                4.3m,
                15,
                new DriverStatus("active"))),
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-44829"),
                "Carlos",
                "Mendoza",
                new Dni("70112233"),
                "A-IIIa",
                new LicenseNumber("Q70112233"),
                new DateOnly(2027, 1, 15),
                13,
                4.5m,
                3,
                new DriverStatus("inactive"))),
            new Driver(new CreateDriverCommand(
                new EmployeeCode("SF-00001"),
                "Alexander Paolo",
                "Justo Yauricasa",
                new Dni("72111890"),
                "A-IIIc",
                new LicenseNumber("Q77443323"),
                new DateOnly(1999, 9, 20),
                10,
                0m,
                5,
                new DriverStatus("active")))
    ];

    /// <summary>
    ///     Gets the sample vehicles of the Fake API.
    /// </summary>
    public static IReadOnlyList<Vehicle> Vehicles() =>
    [
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-7729"),
                new LicensePlate("ABC-1234"),
                "Volvo B11R",
                48,
                new VehicleStatus("in-service"),
                -12.0464,
                -77.0428)),
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-4522"),
                new LicensePlate("GHT-901"),
                "Scania K400",
                52,
                new VehicleStatus("in-service"),
                -12.0389,
                -77.056)),
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-9102"),
                new LicensePlate("PQA-224"),
                "Mercedes OF",
                40,
                new VehicleStatus("available"),
                -12.056,
                -77.03)),
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-0422"),
                new LicensePlate("XYZ-987"),
                "Volvo B11R",
                48,
                new VehicleStatus("in-service"),
                -12.042,
                -77.034)),
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-5590"),
                new LicensePlate("DEF-456"),
                "Scania K400",
                52,
                new VehicleStatus("in-service"),
                -12.062,
                -77.048)),
            new Vehicle(new CreateVehicleCommand(
                new VehicleCode("BUS-3310"),
                new LicensePlate("JKL-321"),
                "Mercedes OF",
                40,
                new VehicleStatus("maintenance"),
                -12.051,
                -77.061))
    ];

    /// <summary>
    ///     Gets the sample assignments of the Fake API.
    /// </summary>
    public static IReadOnlyList<Assignment> Assignments() =>
    [
            new Assignment(new CreateAssignmentCommand(
                1,
                1,
                "R-42 Terminal Norte - Estación Central",
                new TimeOnly(5, 30),
                new AssignmentStatus("active"))),
            new Assignment(new CreateAssignmentCommand(
                2,
                2,
                "Express Aeropuerto",
                new TimeOnly(6, 15),
                new AssignmentStatus("active"))),
            new Assignment(new CreateAssignmentCommand(
                4,
                4,
                "Línea Circunvalación",
                new TimeOnly(7, 0),
                new AssignmentStatus("active"))),
            new Assignment(new CreateAssignmentCommand(
                6,
                5,
                "Jorge Chaves",
                new TimeOnly(21, 43),
                new AssignmentStatus("active")))
    ];

    /// <summary>
    ///     Gets the sample shifts of the Fake API.
    /// </summary>
    public static IReadOnlyList<Shift> Shifts() =>
    [
            new Shift(new CreateShiftCommand(
                1,
                1,
                "R-42",
                "Terminal Norte → Estación Central",
                new DateOnly(2026, 10, 4),
                new DateTime(2026, 10, 4, 8, 0, 0),
                new DateTime(2026, 10, 4, 21, 29, 16),
                11.45,
                12,
                21m,
                0,
                new ShiftStatus("finished"),
                0.0,
                58.0,
                32.0,
                0,
                0)),
            new Shift(new CreateShiftCommand(
                4,
                4,
                "R-01",
                "Ruta Norte 001",
                new DateOnly(2026, 10, 3),
                new DateTime(2026, 10, 3, 22, 0, 0),
                new DateTime(2026, 10, 4, 6, 0, 0),
                142.4,
                1204,
                1450m,
                1,
                new ShiftStatus("finished"),
                0.0,
                88.0,
                52.0,
                0,
                4)),
            new Shift(new CreateShiftCommand(
                4,
                5,
                "L-05",
                "Línea Circunvalación",
                new DateOnly(2026, 10, 2),
                new DateTime(2026, 10, 2, 14, 0, 0),
                new DateTime(2026, 10, 2, 20, 0, 0),
                98.1,
                845,
                1010.5m,
                0,
                new ShiftStatus("finished"),
                0.0,
                64.0,
                38.0,
                0,
                1)),
            new Shift(new CreateShiftCommand(
                4,
                4,
                "R-01",
                "Ruta Norte 001",
                new DateOnly(2026, 10, 1),
                new DateTime(2026, 10, 1, 22, 0, 0),
                new DateTime(2026, 10, 2, 2, 22, 0),
                61.7,
                512,
                620m,
                1,
                new ShiftStatus("interrupted"),
                0.0,
                92.0,
                60.0,
                0,
                5)),
            new Shift(new CreateShiftCommand(
                2,
                2,
                "EX-1",
                "Express Aeropuerto",
                new DateOnly(2026, 10, 3),
                new DateTime(2026, 10, 3, 6, 15, 0),
                new DateTime(2026, 10, 3, 14, 15, 0),
                120.3,
                980,
                1320m,
                0,
                new ShiftStatus("finished"),
                0.0,
                55.0,
                30.0,
                0,
                0)),
            new Shift(new CreateShiftCommand(
                1,
                1,
                "R-42",
                "Terminal Norte → Estación Central",
                new DateOnly(2026, 10, 3),
                new DateTime(2026, 10, 3, 8, 0, 0),
                new DateTime(2026, 10, 3, 15, 42, 15),
                142.4,
                60,
                1450m,
                0,
                new ShiftStatus("finished"),
                0.0,
                62.0,
                35.0,
                0,
                1)),
            new Shift(new CreateShiftCommand(
                4,
                4,
                "R-01",
                "Ruta Norte 001",
                new DateOnly(2026, 10, 4),
                new DateTime(2026, 10, 4, 21, 16, 56),
                new DateTime(2026, 10, 4, 21, 27, 25),
                0.3,
                1,
                1.5m,
                0,
                new ShiftStatus("finished"),
                0.0,
                45.0,
                20.0,
                0,
                0))
    ];

    /// <summary>
    ///     Gets the sample alerts of the Fake API.
    /// </summary>
    public static IReadOnlyList<Alert> Alerts() =>
    [
            new Alert(new CreateAlertCommand(
                new AlertCode("AL-9928"),
                4,
                "Botón de pánico",
                "UNIDAD_MOVIL_04",
                2,
                new AlertStatus("confirmed"),
                new DateTime(2026, 10, 4, 14, 22, 5),
                -12.042,
                -77.034)),
            new Alert(new CreateAlertCommand(
                new AlertCode("AL-9927"),
                2,
                "Puerta abierta en marcha",
                "TERMINAL_CENTRAL",
                3,
                new AlertStatus("critical"),
                new DateTime(2026, 10, 4, 14, 20, 12),
                -12.0389,
                -77.056)),
            new Alert(new CreateAlertCommand(
                new AlertCode("AL-9925"),
                1,
                "Ingreso no autorizado",
                "SISTEMA_RED_WAN",
                3,
                new AlertStatus("critical"),
                new DateTime(2026, 10, 4, 14, 18, 45),
                -12.0464,
                -77.0428)),
            new Alert(new CreateAlertCommand(
                new AlertCode("AL-9920"),
                3,
                "Fallo mecánico",
                "DISP_SEGURIDAD_NORTE",
                2,
                new AlertStatus("confirmed"),
                new DateTime(2026, 10, 4, 14, 15, 22),
                -12.056,
                -77.03)),
            new Alert(new CreateAlertCommand(
                new AlertCode("AL-1953"),
                1,
                "Botón de pánico",
                "CENTRAL_OPERACIONES",
                2,
                new AlertStatus("pending"),
                new DateTime(2026, 10, 5, 22, 39, 6),
                -12.0464,
                -77.0428))
    ];

    /// <summary>
    ///     Gets the sample recipients of the Fake API.
    /// </summary>
    public static IReadOnlyList<Recipient> Recipients() =>
    [
            new Recipient(new CreateRecipientCommand(
                "Ricardo S.",
                "Supervisor de zona centro",
                "SMS / PUSH",
                true)),
            new Recipient(new CreateRecipientCommand(
                "Elena M.",
                "Coordinadora de logística",
                "EMAIL / PUSH",
                true)),
            new Recipient(new CreateRecipientCommand(
                "Marcos T.",
                "Despacho nocturno",
                "SMS",
                false)),
            new Recipient(new CreateRecipientCommand(
                "Sarah J.",
                "Analista de redes",
                "PUSH / WEB",
                true))
    ];

    /// <summary>
    ///     Gets the sample deliveries of the Fake API.
    /// </summary>
    public static IReadOnlyList<Delivery> Deliveries() =>
    [
            new Delivery(new CreateDeliveryCommand(
                "Ricardo S.",
                "SMS / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 10, 12, 0))),
            new Delivery(new CreateDeliveryCommand(
                "Elena M.",
                "EMAIL / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 10, 12, 0))),
            new Delivery(new CreateDeliveryCommand(
                "Sarah J.",
                "PUSH / WEB",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("failed"),
                new DateTime(2026, 10, 4, 10, 12, 0))),
            new Delivery(new CreateDeliveryCommand(
                "Ricardo S.",
                "SMS / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 29))),
            new Delivery(new CreateDeliveryCommand(
                "Elena M.",
                "EMAIL / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 29))),
            new Delivery(new CreateDeliveryCommand(
                "Sarah J.",
                "PUSH / WEB",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 29))),
            new Delivery(new CreateDeliveryCommand(
                "Ricardo S.",
                "SMS / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 34))),
            new Delivery(new CreateDeliveryCommand(
                "Elena M.",
                "EMAIL / PUSH",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 34))),
            new Delivery(new CreateDeliveryCommand(
                "Sarah J.",
                "PUSH / WEB",
                "Fallo de infraestructura",
                "Media",
                new DeliveryStatus("sent"),
                new DateTime(2026, 10, 4, 21, 43, 34)))
    ];
}
