using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;   
using System.Linq;
using AutoMapper;
using API.DTOs;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRespository
{
   // private readonly DbContext _context = context;

    // public async Task<AppUser?> GetUserByIdAsync(int id)
    // {
    //     return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.Id == id);
    // }
    // public async Task<AppUser?> GetUserByUsernameAsync(string username)
    // {
    //     return await context.Users.Include(x => x.Photos)
    //     .SingleOrDefaultAsync(x => x.UserName.ToLower() == username);
    // }
    // public async Task<IEnumerable<AppUser>> GetUsersAsync()
    // {
    //     return await context.Users.ToListAsync();
    // }
    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }   
    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<IEnumerable<MemberDto>> GetAllUserAsync()
    {
        var users = await context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();  
        return users;
    }

    public async Task<MemberDto> GetByUsernameAsync(string username)
    {
        var user = await context.Users.Where(u=>u.UserName == username).
        ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync() ?? throw new Exception("User not found");
        return user;
    }
     public async Task<MemberDto> GetUserByIdAsync(int id)
    {
        var user = await context.Users.Where(u=>u.Id == id).
        ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync() ?? throw new Exception("User not found");
        

        return user;
    }
}