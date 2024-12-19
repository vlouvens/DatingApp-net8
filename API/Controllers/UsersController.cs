using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {

        [HttpGet]//api/users/
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUser(){
           var users = await context.Users.ToListAsync();

           return Ok(users);
        }

        [HttpGet("{id}")]//api/users/id
        public async Task<ActionResult<AppUser>>  GetUser(int id){
           var user = await context.Users.FindAsync(id);
            if ( user == null) return NotFound();
        return user;
        }

    }
}
