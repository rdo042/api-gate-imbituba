using GateAPI.Application;
using GateAPI.Extensions;
using GateAPI.Infra;
using GateAPI.Middlewares;
using System.Reflection;
using System.Text;

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

// Add global exception handler middleware
//app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthorizationResponseMiddleware>();

app.MapControllers();

app.Run();
