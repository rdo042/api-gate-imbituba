using GateAPI.Application;
using GateAPI.Configuration;
using GateAPI.Extensions;
using GateAPI.Infra;
using GateAPI.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services
    .AddHttpContextAccessor()
    .ConfigureCors(builder.Configuration)
    .ConfigureMvcServices()
    .ConfigureSwagger()
    .ConfigureAuthorizationPolicies()
    .AddApplication()
    .AddInfrastructure(builder.Configuration.GetConnectionString("Default"))
    .AddAuth(secretKey);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("GateAPI.Application")));


builder.Services.AddControllers(
    options =>
    {
        // Adicionar convenção para kebab case nas controller (endpoint)
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GATE API v1");
        c.RoutePrefix = string.Empty;
    });

    using var scope = app.Services.CreateScope();
}

// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthorizationResponseMiddleware>();

app.MapControllers();

app.Run();
