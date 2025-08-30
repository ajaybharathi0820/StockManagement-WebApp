using Polisher.API.Extensions;
using Common.Extensions;
using Identity.API.Extensions;
using BagType.API.Extensions;
using Product.API.Extensions;
using StockManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default")!;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
// Configure Serilog via extension
builder.Host.ConfigureSerilog(builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddIdentity(builder.Configuration,connectionString);
builder.Services.AddPolisherModule(connectionString);
builder.Services.AddBagTypeModule(connectionString);
builder.Services.AddProductModule(connectionString);
builder.Services.UseSharedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();   // ✅ must be before UseAuthorization
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapGet("/api/health", () =>
{
    return Results.Ok("API is up and running 🚀");
});

app.UseShared();

app.MapControllers();

app.Run();

