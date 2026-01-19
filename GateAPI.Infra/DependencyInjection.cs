using GateAPI.Application.Providers;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using GateAPI.Infra.Providers;
using GateAPI.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GateAPI.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string? connectionString)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("AppDb");
                options.EnableSensitiveDataLogging();
            });

            // Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<ITipoLacreRepository, TipoLacreRepository>();

            //Providers
            services.AddScoped<ITokenProvider, TokenProvider>();

            //Services
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, byte[] secretKey)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
