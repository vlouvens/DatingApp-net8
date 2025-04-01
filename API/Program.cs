using API;
using API.Extensions;
using API.Middleware;
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
app.Run();
