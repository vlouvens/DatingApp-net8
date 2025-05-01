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
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRespository, UserRepository>();
    //Register the AutoMapper service
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });
    return services;
    }
}
