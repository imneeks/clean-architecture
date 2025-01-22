using CleanArchitecture.APIs.Middleware;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Features.Country.Validations;
using CleanArchitecture.Authentication.Configuration;
using CleanArchitecture.Authentication.Extensions;
using CleanArchitecture.Authentication.Middleware;
using CleanArchitecture.Infrastructure.Extension;
using CleanArchitecture.Logger.Extensions;
using CleanArchitecture.Logger.Middleware;
using CleanArchitecture.Swagger.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();

if (jwtConfig != null)
    builder.Services.AddAuthorizationServices(jwtConfig);

builder.Services.AddControllers();

// Register All Services of Infraastructure Project
builder.Services.AddInfractureServices(); // Pass the current assembly (Infrastructure)


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfigurations(builder.Configuration); // Add Custom Swagger From Project

// MediatR - Start
builder.Services.RegisterCommandsAndHandlers(Assembly.GetAssembly(typeof(ApplicationMarker)));

builder.Services.AddMediatR(cfg =>
{
    // Scan all assemblies in the current app domain for MediatR types
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

// MediatR - End
// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation() // Enables auto-validation
    .AddFluentValidationClientsideAdapters(); // Enables client-side validation (optional)

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationMarker>();


//builder.Services.AddControllers()
//    .AddFluentValidation(config =>
//    {
//        config.RegisterValidatorsFromAssemblyContaining<ApplicationMarker>();
//    });


// Logger - Start
builder.Services.AddLoggerService(builder.Configuration);
builder.Logging.AddLoggerService();
// Logger - End


var localization = builder.Configuration.GetSection("Localization");
if(localization.Exists())
{
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
}

builder.Services.AddSqlDbContext(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.UseMiddleware<CultureMiddleware>(); // Add Culture Middleware
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>(); // Add Logger Middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseCustomSwagger(builder.Configuration);
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();



app.Run();
