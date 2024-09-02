namespace MedPrep.Api.Extensions;

using System.Text;
using MedPrep.Api.Config;
using MedPrep.Api.Context;
using MedPrep.Api.Custom.Identity;
using MedPrep.Api.ExceptionHandlers;
using MedPrep.Api.Exceptions;
using MedPrep.Api.HttpClients;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;
using MedPrep.Api.Repositories;
using MedPrep.Api.Repositories.Common;
using MedPrep.Api.Services;
using MedPrep.Api.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfig(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        _ = services.Configure<RedirectionConfig>(configuration.GetSection(RedirectionConfig.Name));
        _ = services.Configure<PgDbConfig>(configuration.GetSection(PgDbConfig.Name));
        _ = services.Configure<AuthTokenConfig>(configuration.GetSection(AuthTokenConfig.Name));
        _ = services.Configure<EmailConfig>(configuration.GetSection(EmailConfig.Name));
        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        _ = services.AddHttpClient<BunnyStreamHttpClient>(
            BunnyStreamHttpClient.ClientName,
            BunnyStreamHttpClient.Client
        );
        return services;
    }

    public static IServiceCollection AddAppContext(this IServiceCollection services)
    {
        _ = services.AddScoped<IUserStore<Account>, CustomAccountStore>();
        _ = services.AddScoped<IUserStore<Teacher>, CustomTeacherStore>();
        _ = services.AddScoped<IUserStore<User>, CustomUserStore>();
        _ = services.AddDbContext<MedPrepContext>();
        return services;
    }

    public static IServiceCollection AddIdentityContext(this IServiceCollection services)
    {
        _ = services
            .AddIdentity<Account, Role>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<MedPrepContext>()
            .AddDefaultTokenProviders();
        _ = services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<MedPrepContext>()
            .AddDefaultTokenProviders();
        _ = services
            .AddIdentityCore<Teacher>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<MedPrepContext>()
            .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        _ = services
            .AddAuthentication()
            .AddJwtBearer(
                (options) =>
                {
                    var authConfig =
                        configuration.Get<AuthTokenConfig>()
                        ?? throw new AppException("Auth config not found");
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authConfig!.Issuer,
                        ValidAudience = authConfig!.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(authConfig!.AccessTokenSecret)
                        )
                    };
                }
            );
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddScoped<ITeacherRepository, TeacherRepository>();
        _ = services.AddScoped<IVideoRepository, VideoRepository>();
        _ = services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();
        _ = services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        _ = services.AddScoped<IVideoSourceRepository, VideoSourceRepository>();
        _ = services.AddScoped<ISubtitleSourceRepository, SubtitleSourceRepository>();
        _ = services.AddScoped<ILicenseRepository, LicenseRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IEmailService, EmailService>();
        _ = services.AddScoped<IJwtService, JwtService>();
        _ = services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddExceptionsHandlers(this IServiceCollection services)
    {
        _ = services.AddExceptionHandler<BadRequestExceptionHandler>();
        _ = services.AddExceptionHandler<ConflictExceptionHandler>();
        _ = services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        _ = services.AddExceptionHandler<NotFoundExceptionHandler>();
        _ = services.AddExceptionHandler<ConflictExceptionHandler>();
        _ = services.AddExceptionHandler<GlobalExceptionHandler>();
        _ = services.AddProblemDetails();
        return services;
    }
}
