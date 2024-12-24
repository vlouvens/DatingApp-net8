using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser appUser)
    {
        var TokenKey = config["TokenKey"] ?? throw new Exception("Cannot access the token key");
        if(TokenKey.Length < 64 ) throw new Exception("The token key needs to be more longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var claims = new List<Claim>{
            new (ClaimTypes.NameIdentifier,appUser.UserName)
        };

        var keyDescriptor = new SecurityTokenDescriptor{
               Subject = new ClaimsIdentity(claims),
               Expires = DateTime.UtcNow.AddMinutes(120),
               SigningCredentials = creds
        };

        var tokenHanlder = new JwtSecurityTokenHandler();
        var token = tokenHanlder.CreateToken(keyDescriptor);
        return tokenHanlder.WriteToken(token);

    }
}
