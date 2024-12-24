using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService token) : BaseAPIController
    {
        [HttpPost("register")] //api/account/register
        public async Task<ActionResult<AppUser>> Register([FromBody] AccountRegisterDto userDto)
        {
    
            if (await UsernameExist(userDto.Username))
            {
                return BadRequest("Username already taken");
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = userDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                PasswordSalt = hmac.Key
            };
            context.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        [HttpPost("login")] //api/account/login
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
    
          var user = await context.Users.FirstOrDefaultAsync(x=>x.UserName.ToLower() == loginDto.Username);
          if(user == null ) return Unauthorized("Username invalid");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
           
           for(int i=0; i < ComputeHash.Length; i++){
            if(ComputeHash[i] != user.PasswordHash[i]){
                return Unauthorized("Password invalid");
            }
           }
            return new UserDto{
                Username = user.UserName,
                Token = token.CreateToken(user)
            };
        }
        public async Task<bool> UsernameExist(string username)
        {
            return await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}
