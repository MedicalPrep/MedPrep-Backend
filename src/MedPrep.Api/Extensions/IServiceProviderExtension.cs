namespace MedPrep.Api.Extensions;

using System.Threading.Tasks;
using MedPrep.Api.Models;
using Microsoft.AspNetCore.Identity;

public static class IServiceProviderExtension
{
    public static async Task<IServiceProvider> AddRolesAsync(this IServiceProvider services)
    {
        using (var serviceScope = services.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            foreach (var role in (UserRoles[])Enum.GetValues(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    _ = await roleManager.CreateAsync(new Role(role.ToString()));
                }
            }
        }

        return services;
    }
}
