using Polisher.API.Extensions;
using Common.Extensions;
using Identity.API.Extensions;
using BagType.API.Extensions;
using Product.API.Extensions;
using Production.API.Extensions;
using StockManagement.API.Extensions;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default")!;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StockManagement API",
        Version = "v1"
    });

    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Paste your access token only. The 'Bearer' prefix is added automatically."
    };
    options.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };
    options.AddSecurityRequirement(securityRequirement);
});
// Configure Serilog via extension
builder.Host.ConfigureSerilog(builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddIdentity(builder.Configuration,connectionString);
builder.Services.AddPolisherModule(connectionString);
builder.Services.AddBagTypeModule(connectionString);
builder.Services.AddProductModule(connectionString);
builder.Services.AddProductionModule(connectionString);
builder.Services.UseSharedServices();
// Add CORS policy for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

// Seed Identity data (roles and default admin user)
await SeedDataAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS before authentication/authorization
app.UseCors("AllowFrontend");

app.UseAuthentication();   // ✅ must be before UseAuthorization
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapGet("/api/health", () =>
{
    return Results.Ok("API is up and running 🚀");
});

app.MapGet("/api/seed-identity", async (IServiceProvider services) =>
{
    using var scope = services.CreateScope();
    try
    {
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        await IdentityDataSeeder.SeedAsync(identityContext);
        return Results.Ok("✅ Identity data seeded successfully");
    }
    catch (Exception ex)
    {
        return Results.Problem($"❌ Error seeding Identity data: {ex.Message}");
    }
});

app.MapGet("/api/check-identity", async (IServiceProvider services) =>
{
    using var scope = services.CreateScope();
    try
    {
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        var rolesCount = await identityContext.Roles.CountAsync();
        var usersCount = await identityContext.Users.CountAsync();
        return Results.Ok(new { Roles = rolesCount, Users = usersCount });
    }
    catch (Exception ex)
    {
        return Results.Problem($"❌ Error checking Identity data: {ex.Message}");
    }
});

app.UseShared();

app.MapControllers();

app.Run();

static async Task SeedDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    try
    {
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        await IdentityDataSeeder.SeedAsync(identityContext);
        Console.WriteLine("✅ Identity data seeded successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error seeding Identity data: {ex.Message}");
        Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
}

