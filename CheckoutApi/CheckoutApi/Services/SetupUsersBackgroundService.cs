
using CheckoutApi.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CheckoutApi.Services;

public class SetupUsersBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SetupUsersBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            UserManager<AppUser> userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            RoleManager<IdentityRole> roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var user = new AppUser { UserName = "morganmwj@gmail.com", Email = "morganmwj@gmail.com", FirstName = "Morgan", LastName = "Jones" };
            var c1 = userManager.CreateAsync(user, "!MyPassword321!");

            await Task.WhenAll(c1);

            var adminRoleExists = await roleManager.RoleExistsAsync(Roles.Admin);
            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole(Roles.Admin);
                await roleManager.CreateAsync(adminRole);
            }

            var a1 = userManager.AddToRoleAsync(user, Roles.Admin);
            var a2 = userManager.AddClaimAsync(user, new Claim(Claims.CustomClaim, "value"));

            await Task.WhenAll(a1, a2);
        }

    }
}
