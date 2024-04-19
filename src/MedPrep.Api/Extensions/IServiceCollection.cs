namespace MedPrep.Api.Extensions;

using MedPrep.Api.Config;
using MedPrep.Api.Context;

public static class IService
{
    public static IServiceCollection AddAppConfig(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        _ = services.Configure<PgDbConfig>(configuration.GetSection(PgDbConfig.Name));
        return services;
    }

    public static IServiceCollection AddAppContext(this IServiceCollection services)
    {
        _ = services.AddDbContext<MedPrepContext>();
        return services;
    }
}
