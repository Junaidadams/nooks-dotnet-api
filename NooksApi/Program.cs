using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NooksApi.Constants;
using NooksApi.Data;
using NooksApi.Data.Entities;
using NooksApi.Extensions;
using NooksApi.Handlers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiLogging();

var allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:5173" }
    : new[] { "https://nooks.co.za" };

builder.Services.AddCors(options =>
{
    options.AddPolicy(ConfigurationConstants.CorsPolicy, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<NooksDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionString),
        npgsqlOptions => npgsqlOptions.UseNetTopologySuite());
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<NooksDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
        ? CookieSecurePolicy.SameAsRequest
        : CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
});

builder.Services.AddAuthorization();

builder.Services.AddOptions();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddApiRateLimiting();
builder.Services.AddApiOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (Environment.GetEnvironmentVariable("NOOKS_SKIP_MIGRATIONS") != "true")
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<NooksDbContext>();
        db.Database.Migrate();
    }
}

app.UseExceptionHandler();
app.UseCors(ConfigurationConstants.CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

app.Run();
