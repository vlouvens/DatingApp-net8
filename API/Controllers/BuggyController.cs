using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class BuggyController(DataContext context) : BaseAPIController{
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth(){
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = context.Users.Find(-1);
            if (thing == null) return NotFound();
            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
            var thing = context.Users.Find(-1);
            var thingToReturn = thing ?? throw new Exception("Bad thing has happened");
            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult<string> GetBadRequest(int id)
        {
            return BadRequest("This was not a good request");
        }
    } 
}