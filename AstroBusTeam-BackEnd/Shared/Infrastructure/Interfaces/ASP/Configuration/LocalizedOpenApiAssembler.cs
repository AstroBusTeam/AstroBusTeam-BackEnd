using SecurityBus.API.Resources;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;

namespace SecurityBus.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
///     Assembles localized OpenAPI metadata (summaries, descriptions, and response texts)
///     for every REST endpoint and resource, resolving the texts by naming convention.
/// </summary>
/// <remarks>
///     For an action named <c>CreateDriver</c> the transformer looks for the resource keys
///     <c>CreateDriverSummary</c>, <c>CreateDriverDescription</c> and <c>CreateDriver{StatusCode}</c>.
///     For a schema such as <c>CreateDriverResource</c> it looks for <c>CreateDriverResourceSchema</c>, and for each of
///     its properties it looks for <c>{Base}{Property}</c> (for example <c>DriverEmployeeCode</c>).
///     Keys that are not defined are simply skipped.
/// </remarks>
public static class LocalizedOpenApiAssembler
{
    /// <summary>
    ///     Localizes the summary, description, operation id and response descriptions of known operations.
    /// </summary>
    /// <param name="operation">The OpenAPI operation being transformed.</param>
    /// <param name="context">The transformer context, used to resolve the localizer and action descriptor.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A completed task.</returns>
    public static Task TransformOperationAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.Description.ActionDescriptor is not ControllerActionDescriptor action)
            return Task.CompletedTask;

        var localizer = context.ApplicationServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        var summary = localizer[$"{action.ActionName}Summary"];
        if (summary.ResourceNotFound) return Task.CompletedTask;

        operation.OperationId = action.ActionName;
        operation.Summary = summary.Value;
        operation.Description = localizer[$"{action.ActionName}Description"].Value;
        if (operation.Responses is null) return Task.CompletedTask;

        foreach (var statusCode in operation.Responses.Keys.ToList())
        {
            var text = localizer[$"{action.ActionName}{statusCode}"];
            if (text.ResourceNotFound) continue;
            if (operation.Responses.TryGetValue(statusCode, out var response) &&
                response is OpenApiResponse openApiResponse)
                openApiResponse.Description = text.Value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Localizes the description of known resource schemas and, from the owner schema, of their properties.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being transformed.</param>
    /// <param name="context">The transformer context, used to resolve the localizer and type information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A completed task.</returns>
    public static Task TransformSchemaAsync(
        OpenApiSchema schema,
        OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        // Property schemas are handled while transforming their owner schema, because the owner type is not
        // available in the property's own transformer context.
        if (context.JsonPropertyInfo is not null) return Task.CompletedTask;

        var localizer = context.ApplicationServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        var type = context.JsonTypeInfo.Type;

        var schemaText = localizer[$"{type.Name}Schema"];
        if (!schemaText.ResourceNotFound) schema.Description = schemaText.Value;
        if (schema.Properties is null) return Task.CompletedTask;

        var baseName = ResourceBaseName(type);
        foreach (var (propertyName, property) in schema.Properties)
        {
            var text = localizer[$"{baseName}{char.ToUpperInvariant(propertyName[0])}{propertyName[1..]}"];
            if (!text.ResourceNotFound && property is OpenApiSchema openApiSchema)
                openApiSchema.Description = text.Value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Gets the base name of a resource type: <c>CreateDriverResource</c>, <c>UpdateDriverResource</c> and
    ///     <c>DriverResource</c> all share the base name <c>Driver</c>.
    /// </summary>
    private static string ResourceBaseName(Type type)
    {
        var name = type.Name;
        if (name.EndsWith("Resource", StringComparison.Ordinal)) name = name[..^"Resource".Length];
        foreach (var prefix in new[] { "Create", "Update" })
            if (name.StartsWith(prefix, StringComparison.Ordinal) && name.Length > prefix.Length &&
                char.IsUpper(name[prefix.Length]))
                return name[prefix.Length..];
        return name;
    }
}
