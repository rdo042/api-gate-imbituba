using GateAPI.Authorization.Permissions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GateAPI.Tests.Controllers
{
    public class ApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "Test", _ => { });

                var authBuilder = services.AddAuthorizationBuilder();

                foreach (var permissao in PermissionConstants.GetAll)
                {
                    authBuilder.AddPolicy(permissao, policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(
                            new HasPermissionRequirement(permissao));
                    });
                }
            });
        }
    }
}
