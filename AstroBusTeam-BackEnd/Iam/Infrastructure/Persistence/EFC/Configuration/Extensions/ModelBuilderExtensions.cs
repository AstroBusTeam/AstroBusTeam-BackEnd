using Microsoft.EntityFrameworkCore;

namespace AstroBusTeamBackEnd.Iam.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions of the IAM bounded context
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the IAM configuration to the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.ApplyUserConfiguration();
    }
}
