using AstroBusTeamBackEnd.Alerts.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Fleet.Domain.Model.Commands;
using AstroBusTeamBackEnd.Fleet.Domain.Model.ValueObjects;
using AstroBusTeamBackEnd.Iam.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Iam.Domain.Model.Commands;
using AstroBusTeamBackEnd.Iam.Domain.Services;
using AstroBusTeamBackEnd.Operations.Domain.Model.Aggregates;
using AstroBusTeamBackEnd.Operations.Domain.Model.Commands;
using AstroBusTeamBackEnd.Operations.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Sample data to try the API right away
/// </summary>
/// <remarks>
///     Every aggregate is created through its own creation command, so the sample data goes through the same domain
///     validation as any request. The identifiers follow the insertion order of <see cref="DatabaseSeeder"/>, so the
///     foreign keys of one list point to the position of the rows in the others (the first row has id 1).
/// </remarks>
public static class SampleData
{
    /// <summary>
    ///     Gets the sample control centers.
    /// </summary>
    public static IReadOnlyList<ControlCenter> ControlCenters() =>
    [
        new ControlCenter(new CreateControlCenterCommand("CC-LIMA-01", "Centro de Control Lima Centro", "Lima Centro", true)),
        new ControlCenter(new CreateControlCenterCommand("CC-LIMA-02", "Centro de Control Lima Norte", "Lima Norte", true)),
        new ControlCenter(new CreateControlCenterCommand("CC-CALLAO-01", "Centro de Control Callao", "Callao", true))
    ];

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
                "987654301",
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
                "987654302",
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
                "987654303",
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
                "987654304",
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
                "987654305",
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
                "987654306",
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
    ///     Gets the sample routes.
    /// </summary>
    public static IReadOnlyList<BusRoute> BusRoutes() =>
    [
        new BusRoute(new CreateBusRouteCommand("R-42", "Terminal Norte - Estación Central", "Terminal Norte", "Estación Central", 11.45m, true)),
        new BusRoute(new CreateBusRouteCommand("EX-1", "Express Aeropuerto", "Terminal Central", "Aeropuerto Jorge Chávez", 28.6m, true)),
        new BusRoute(new CreateBusRouteCommand("L-05", "Línea Circunvalación", "Plaza Mayor", "Plaza Mayor", 34.2m, true)),
        new BusRoute(new CreateBusRouteCommand("R-01", "Ruta Norte 001", "Terminal Norte", "Terminal Sur", 142.4m, true)),
        new BusRoute(new CreateBusRouteCommand("R-77", "Ruta Jorge Chávez", "Terminal Sur", "Aeropuerto Jorge Chávez", 21.8m, true))
    ];

    /// <summary>
    ///     Gets the sample users. The passwords are only for local tests.
    /// </summary>
    public static IReadOnlyList<User> Users() =>
    [
        new User(new CreateUserCommand("admin", PasswordHasher.Hash("Admin#2026"), "admin", null, true)),
        new User(new CreateUserCommand("operador", PasswordHasher.Hash("Operador#2026"), "operator", null, true)),
        new User(new CreateUserCommand("msilva", PasswordHasher.Hash("Conductor#2026"), "driver", 1, true))
    ];

    /// <summary>
    ///     Gets the sample assignments.
    /// </summary>
    public static IReadOnlyList<Assignment> Assignments() =>
    [
        new Assignment(new CreateAssignmentCommand(1, 1, 1, new TimeOnly(5, 30), new DateOnly(2026, 9, 1), null, new AssignmentStatus("active"), 1)),
        new Assignment(new CreateAssignmentCommand(2, 2, 2, new TimeOnly(6, 15), new DateOnly(2026, 9, 1), null, new AssignmentStatus("active"), 1)),
        new Assignment(new CreateAssignmentCommand(4, 4, 4, new TimeOnly(7, 0), new DateOnly(2026, 9, 15), null, new AssignmentStatus("active"), 1)),
        new Assignment(new CreateAssignmentCommand(6, 5, 5, new TimeOnly(21, 43), new DateOnly(2026, 10, 1), null, new AssignmentStatus("active"), 2)),
        new Assignment(new CreateAssignmentCommand(4, 5, 3, new TimeOnly(14, 0), new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 2), new AssignmentStatus("inactive"), 1))
    ];

    /// <summary>
    ///     Gets the sample shifts.
    /// </summary>
    public static IReadOnlyList<Shift> Shifts() =>
    [
        new Shift(new CreateShiftCommand(1, 1, 1, 1, new DateOnly(2026, 10, 4), new DateTime(2026, 10, 4, 8, 0, 0), new DateTime(2026, 10, 4, 21, 29, 16), 11.45, 12, 21m, new ShiftStatus("finished"), 32.0, 58.0, 0)),
        new Shift(new CreateShiftCommand(3, 4, 4, 4, new DateOnly(2026, 10, 3), new DateTime(2026, 10, 3, 22, 0, 0), new DateTime(2026, 10, 4, 6, 0, 0), 142.4, 1204, 1450m, new ShiftStatus("finished"), 52.0, 88.0, 4)),
        new Shift(new CreateShiftCommand(5, 4, 5, 3, new DateOnly(2026, 10, 2), new DateTime(2026, 10, 2, 14, 0, 0), new DateTime(2026, 10, 2, 20, 0, 0), 98.1, 845, 1010.5m, new ShiftStatus("finished"), 38.0, 64.0, 1)),
        new Shift(new CreateShiftCommand(3, 4, 4, 4, new DateOnly(2026, 10, 1), new DateTime(2026, 10, 1, 22, 0, 0), new DateTime(2026, 10, 2, 2, 22, 0), 61.7, 512, 620m, new ShiftStatus("interrupted"), 60.0, 92.0, 5)),
        new Shift(new CreateShiftCommand(2, 2, 2, 2, new DateOnly(2026, 10, 3), new DateTime(2026, 10, 3, 6, 15, 0), new DateTime(2026, 10, 3, 14, 15, 0), 120.3, 980, 1320m, new ShiftStatus("finished"), 30.0, 55.0, 0)),
        new Shift(new CreateShiftCommand(1, 1, 1, 1, new DateOnly(2026, 10, 3), new DateTime(2026, 10, 3, 8, 0, 0), new DateTime(2026, 10, 3, 15, 42, 15), 142.4, 60, 1450m, new ShiftStatus("finished"), 35.0, 62.0, 1)),
        new Shift(new CreateShiftCommand(3, 4, 4, 4, new DateOnly(2026, 10, 4), new DateTime(2026, 10, 4, 21, 16, 56), new DateTime(2026, 10, 4, 21, 27, 25), 0.3, 1, 1.5m, new ShiftStatus("finished"), 20.0, 45.0, 0))
    ];

    /// <summary>
    ///     Gets the sample vehicle positions.
    /// </summary>
    public static IReadOnlyList<VehiclePosition> VehiclePositions() =>
    [
        new VehiclePosition(new CreateVehiclePositionCommand(1, 1, -12.0464, -77.0428, 32.0, new DateTime(2026, 10, 4, 8, 5, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(1, 1, -12.0500, -77.0400, 41.5, new DateTime(2026, 10, 4, 8, 35, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(4, 2, -12.0420, -77.0340, 52.0, new DateTime(2026, 10, 3, 23, 10, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(4, 2, -12.0450, -77.0310, 60.5, new DateTime(2026, 10, 4, 1, 20, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(5, 3, -12.0620, -77.0480, 38.0, new DateTime(2026, 10, 2, 15, 0, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(2, 5, -12.0389, -77.0560, 55.0, new DateTime(2026, 10, 3, 7, 0, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(2, 5, -12.0420, -77.0500, 30.0, new DateTime(2026, 10, 3, 9, 30, 0))),
        new VehiclePosition(new CreateVehiclePositionCommand(4, 7, -12.0420, -77.0340, 20.0, new DateTime(2026, 10, 4, 21, 20, 0)))
    ];

    /// <summary>
    ///     Gets the sample alert types.
    /// </summary>
    public static IReadOnlyList<AlertType> AlertTypes() =>
    [
        new AlertType(new CreateAlertTypeCommand("Botón de pánico", "high", true)),
        new AlertType(new CreateAlertTypeCommand("Puerta abierta en marcha", "high", false)),
        new AlertType(new CreateAlertTypeCommand("Ingreso no autorizado", "critical", false)),
        new AlertType(new CreateAlertTypeCommand("Fallo mecánico", "medium", false)),
        new AlertType(new CreateAlertTypeCommand("Fallo de infraestructura", "medium", false)),
        new AlertType(new CreateAlertTypeCommand("Desvío de ruta", "medium", false))
    ];

    /// <summary>
    ///     Gets the sample notification channels.
    /// </summary>
    public static IReadOnlyList<NotificationChannel> NotificationChannels() =>
    [
        new NotificationChannel(new CreateNotificationChannelCommand("sms")),
        new NotificationChannel(new CreateNotificationChannelCommand("push")),
        new NotificationChannel(new CreateNotificationChannelCommand("email")),
        new NotificationChannel(new CreateNotificationChannelCommand("web"))
    ];

    /// <summary>
    ///     Gets the sample recipients with their notification channels.
    /// </summary>
    public static IReadOnlyList<Recipient> Recipients() =>
    [
        new Recipient(new CreateRecipientCommand("Ricardo S.", "Supervisor de zona centro", "ricardo.s@securitybus.pe", "987000111", 1, true, [1, 2])),
        new Recipient(new CreateRecipientCommand("Elena M.", "Coordinadora de logística", "elena.m@securitybus.pe", "987000222", 1, true, [2, 3])),
        new Recipient(new CreateRecipientCommand("Marcos T.", "Despacho nocturno", "marcos.t@securitybus.pe", "987000333", 2, false, [1])),
        new Recipient(new CreateRecipientCommand("Sarah J.", "Analista de redes", "sarah.j@securitybus.pe", "987000444", 3, true, [2, 4]))
    ];

    /// <summary>
    ///     Gets the sample alerts.
    /// </summary>
    public static IReadOnlyList<Alert> Alerts() =>
    [
        new Alert(new CreateAlertCommand(new AlertCode("AL-9928"), 4, 4, 7, 1, 1, 2, new AlertStatus("confirmed"), new DateTime(2026, 10, 4, 14, 22, 5), -12.042, -77.034, new DateTime(2026, 10, 4, 14, 23, 10), 2, "Unidad contactada por radio.")),
        new Alert(new CreateAlertCommand(new AlertCode("AL-9927"), 2, 2, 5, 2, 2, 3, new AlertStatus("critical"), new DateTime(2026, 10, 4, 14, 20, 12), -12.0389, -77.056, null, null, null)),
        new Alert(new CreateAlertCommand(new AlertCode("AL-9925"), 1, 1, 6, 3, 4, 3, new AlertStatus("critical"), new DateTime(2026, 10, 4, 14, 18, 45), -12.0464, -77.0428, null, null, null)),
        new Alert(new CreateAlertCommand(new AlertCode("AL-9920"), 3, null, null, 4, 3, 2, new AlertStatus("confirmed"), new DateTime(2026, 10, 4, 14, 15, 22), -12.056, -77.03, new DateTime(2026, 10, 4, 14, 16, 40), 2, "Unidad sin conductor asignado, revisada en patio.")),
        new Alert(new CreateAlertCommand(new AlertCode("AL-1953"), 1, 1, 1, 1, 1, 2, new AlertStatus("pending"), new DateTime(2026, 10, 5, 22, 39, 6), -12.0464, -77.0428, null, null, null))
    ];

    /// <summary>
    ///     Gets the sample alert attempts.
    /// </summary>
    public static IReadOnlyList<AlertAttempt> AlertAttempts() =>
    [
        new AlertAttempt(new CreateAlertAttemptCommand(1, 1, new DateTime(2026, 10, 4, 14, 22, 6), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(1, 2, new DateTime(2026, 10, 4, 14, 22, 36), "success")),
        new AlertAttempt(new CreateAlertAttemptCommand(2, 1, new DateTime(2026, 10, 4, 14, 20, 13), "no_answer")),
        new AlertAttempt(new CreateAlertAttemptCommand(2, 2, new DateTime(2026, 10, 4, 14, 20, 43), "no_answer")),
        new AlertAttempt(new CreateAlertAttemptCommand(2, 3, new DateTime(2026, 10, 4, 14, 21, 13), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(3, 1, new DateTime(2026, 10, 4, 14, 18, 46), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(3, 2, new DateTime(2026, 10, 4, 14, 19, 16), "no_answer")),
        new AlertAttempt(new CreateAlertAttemptCommand(3, 3, new DateTime(2026, 10, 4, 14, 19, 46), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(4, 1, new DateTime(2026, 10, 4, 14, 15, 23), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(4, 2, new DateTime(2026, 10, 4, 14, 15, 53), "success")),
        new AlertAttempt(new CreateAlertAttemptCommand(5, 1, new DateTime(2026, 10, 5, 22, 39, 7), "failed")),
        new AlertAttempt(new CreateAlertAttemptCommand(5, 2, new DateTime(2026, 10, 5, 22, 39, 37), "no_answer"))
    ];

    /// <summary>
    ///     Gets the sample deliveries.
    /// </summary>
    public static IReadOnlyList<Delivery> Deliveries() =>
    [
        new Delivery(new CreateDeliveryCommand(1, 1, 1, 1, "high", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 22, 6), null)),
        new Delivery(new CreateDeliveryCommand(1, 2, 3, 1, "high", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 22, 6), null)),
        new Delivery(new CreateDeliveryCommand(1, 4, 2, 1, "high", new DeliveryStatus("failed"), new DateTime(2026, 10, 4, 14, 22, 6), "Dispositivo sin conexión")),
        new Delivery(new CreateDeliveryCommand(2, 1, 1, 2, "high", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 20, 13), null)),
        new Delivery(new CreateDeliveryCommand(2, 2, 3, 2, "high", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 20, 13), null)),
        new Delivery(new CreateDeliveryCommand(2, 4, 2, 2, "high", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 20, 13), null)),
        new Delivery(new CreateDeliveryCommand(3, 1, 1, 3, "critical", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 18, 46), null)),
        new Delivery(new CreateDeliveryCommand(3, 2, 3, 3, "critical", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 18, 46), null)),
        new Delivery(new CreateDeliveryCommand(3, 4, 2, 3, "critical", new DeliveryStatus("sent"), new DateTime(2026, 10, 4, 14, 18, 51), null))
    ];
}
