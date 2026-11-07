using AstroBusTeamBackEnd.Alerts.Application.Internal.CommandServices;
using AstroBusTeamBackEnd.Alerts.Application.Internal.QueryServices;
using AstroBusTeamBackEnd.Alerts.Application.Services;
using AstroBusTeamBackEnd.Alerts.Domain.Repositories;
using AstroBusTeamBackEnd.Alerts.Infrastructure.Persistence.EFC.Repositories;
using AstroBusTeamBackEnd.Fleet.Application.Internal.CommandServices;
using AstroBusTeamBackEnd.Fleet.Application.Internal.QueryServices;
using AstroBusTeamBackEnd.Fleet.Application.Services;
using AstroBusTeamBackEnd.Fleet.Domain.Repositories;
using AstroBusTeamBackEnd.Fleet.Infrastructure.Persistence.EFC.Repositories;
using AstroBusTeamBackEnd.Fleet.Interfaces.ACL.Services;
using AstroBusTeamBackEnd.Fleet.Interfaces.ACL;
using AstroBusTeamBackEnd.Iam.Application.Internal.CommandServices;
using AstroBusTeamBackEnd.Iam.Application.Services;
using AstroBusTeamBackEnd.Operations.Application.Internal.CommandServices;
using AstroBusTeamBackEnd.Operations.Application.Internal.QueryServices;
using AstroBusTeamBackEnd.Operations.Application.Services;
using AstroBusTeamBackEnd.Operations.Domain.Repositories;
using AstroBusTeamBackEnd.Operations.Infrastructure.Persistence.EFC.Repositories;
using AstroBusTeamBackEnd.Resources;
using AstroBusTeamBackEnd.Shared.Domain.Repositories;
using AstroBusTeamBackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using AstroBusTeamBackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Localization Configuration
builder.Services.AddLocalization();

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization(options =>
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource)))
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var localizer = context.HttpContext.RequestServices
                .GetRequiredService<IStringLocalizer<SharedResource>>();
            var problemDetailsFactory = context.HttpContext.RequestServices
                .GetRequiredService<ProblemDetailsFactory>();
            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState,
                statusCode: StatusCodes.Status400BadRequest,
                title: localizer["InvalidModelStateTitle"].Value);

            var result = new BadRequestObjectResult(problemDetails);
            result.ContentTypes.Add("application/problem+json");
            return result;
        };
    });

// Register RFC 7807 ProblemDetails payloads for centralized exception handling.
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        if (context.ProblemDetails.Status is null or >= 500)
        {
            var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
            context.ProblemDetails.Title ??= localizer["UnexpectedServerError"].Value;
            context.ProblemDetails.Detail ??= localizer["UnexpectedErrorProcessingRequest"].Value;
        }
    };
});

// CORS: the web application (Vue) is served from a different origin than this API.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options => options.AddPolicy("Frontend", policy =>
{
    // In Development any origin is allowed, so the frontend works whatever port Vite picks.
    if (builder.Environment.IsDevelopment() || allowedOrigins.Length == 0)
        policy.AllowAnyOrigin();
    else
        policy.WithOrigins(allowedOrigins);

    policy.AllowAnyHeader().AllowAnyMethod();
}));

// Generate the OpenAPI document with ASP.NET Core's built-in support.
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer(LocalizedOpenApiAssembler.TransformOperationAsync);
    options.AddSchemaTransformer(LocalizedOpenApiAssembler.TransformSchemaAsync);
});

// Add Database Connection

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Fleet Bounded Context Injection Configuration
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverCommandService, DriverCommandService>();
builder.Services.AddScoped<IDriverQueryService, DriverQueryService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCommandService, VehicleCommandService>();
builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentCommandService, AssignmentCommandService>();
builder.Services.AddScoped<IAssignmentQueryService, AssignmentQueryService>();
builder.Services.AddScoped<IFleetContextFacade, FleetContextFacade>();

// Operations Bounded Context Injection Configuration
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftCommandService, ShiftCommandService>();
builder.Services.AddScoped<IShiftQueryService, ShiftQueryService>();

// Alerts Bounded Context Injection Configuration
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();
builder.Services.AddScoped<IRecipientRepository, RecipientRepository>();
builder.Services.AddScoped<IRecipientCommandService, RecipientCommandService>();
builder.Services.AddScoped<IRecipientQueryService, RecipientQueryService>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryCommandService, DeliveryCommandService>();
builder.Services.AddScoped<IDeliveryQueryService, DeliveryQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IDriverVerificationCommandService, DriverVerificationCommandService>();
var app = builder.Build();

// Create the schema when it does not exist yet and, optionally, load the sample data of the Fake API.
// To work with EF Core migrations instead, run `dotnet ef migrations add InitialCreate` and replace
// EnsureCreated() with Migrate().
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (builder.Configuration.GetValue<bool>("Database:SeedSampleData"))
        await DatabaseSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Localization must run before Swagger so generated
// documentation follows the request culture.
string[] supportedCultures = ["en", "en-US", "es", "es-PE"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);

// In Development the frontend calls the API over plain http; redirecting to https would break CORS preflight requests.
if (!app.Environment.IsDevelopment()) app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthorization();

// OpenAPI and Scalar are kept available in all environments to preserve the existing API explorer behavior.
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

app.Run();
