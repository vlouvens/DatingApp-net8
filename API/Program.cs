using API;
using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Add middleware to the request pipeline that will handle exceptions
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseCors(options =>
    options
        .WithOrigins("http://localhost:4200", "https://localhost:4200") // Specify allowed origins
        .AllowAnyHeader()
        .AllowAnyMethod()
);

//seed data
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataContext>();
try
{
    await context.Database.MigrateAsync();
    await SeedData.SeedDataAsync(context);
}
catch (Exception ex)
{

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}
app.Run();
