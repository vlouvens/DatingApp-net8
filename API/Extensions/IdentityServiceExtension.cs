using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
    {
    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });
    return services;
    }
}