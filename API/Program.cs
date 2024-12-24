using API.Extensions;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseCors(options =>
    options
        .WithOrigins("http://localhost:4200", "https://localhost:4200") // Specify allowed origins
        .AllowAnyHeader()
        .AllowAnyMethod()
);
app.Run();
