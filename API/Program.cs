using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseCors(options =>
    options
        .WithOrigins("http://localhost:4200", "https://localhost:4200") // Specify allowed origins
        .AllowAnyHeader()
        .AllowAnyMethod()
);


app.Run();
