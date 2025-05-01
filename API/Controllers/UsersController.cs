using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController(IUserRespository repos) : BaseAPIController
    {
        [HttpGet]//api/users/
        public async Task <ActionResult<IEnumerable<MemberDto>>> GetUsers(){
           var users = await repos.GetAllUserAsync();
            if ( users == null) return NotFound();

           return Ok(users);
        }
        
        [HttpGet("{id:int}")]//api/users/id
        public async Task<ActionResult<MemberDto>>  GetUserByID(int id){
           var user = await repos.GetUserByIdAsync(id);
            if ( user == null) return NotFound();
        return user;
        }

        [HttpGet("{username}")]//api/users/username
        public async Task<ActionResult<MemberDto>>  GetUserByUsername(string username){
           var user = await repos.GetByUsernameAsync(username);
            if ( user == null) return NotFound();
        return Ok(user);
        }

    }
}
