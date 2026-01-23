using GateAPI.Authorization.Permissions;
using GateAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GateAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins(allowedOrigins!)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthorizationPolicies(this IServiceCollection services)
        {
            var authBuilder = services.AddAuthorizationBuilder();

            foreach (var permissao in PermissionConstants.GetAll)
            {
                authBuilder.AddPolicy(permissao, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new HasPermissionRequirement(permissao));
                });
            }

            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();
            return services;
        }

        public static IServiceCollection ConfigureMvcServices(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                //options.Filters.Add<ValidateModelAttribute>();
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(
                        new KebabCaseTransformer()
                    )
                );
            })
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter()
                );
            });

            ////services.AddFluentValidationAutoValidation();
            ///

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // automatizar kebab-case para todas as controllers
                options.TagActionsBy(api =>
                {
                    var controller = api.GroupName ?? api.ActionDescriptor.RouteValues["controller"];
                    return new[] {
                        Regex.Replace(controller, "(?<!^)([A-Z])", "-$1").ToLower()
                    };
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gate API",
                    Version = "v1",
                    Description = "Documentação da API interna",
                    Contact = new OpenApiContact
                    {
                        Name = "Yve Zulatto",
                        Email = "yve.zulatto@supero.com"
                    }
                });

                // Adiciona o esquema de segurança JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no campo abaixo usando o formato: Bearer {seu_token}"
                });

                // Aplica o esquema de segurança em todos os endpoints protegidos
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

    }
}
