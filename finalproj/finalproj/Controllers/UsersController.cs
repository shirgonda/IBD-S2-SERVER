using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using finalproj.BL;

namespace finalproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            User user = new User();
            return user.Read(); 
        }

        //GET api/<UsersController>/5
        //[HttpGet("{}")]
        //public IActionResult Get()
        //{
        //    
        //}

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {

            if (user.Insert()) // Assuming Insert() returns true if the user is successfully inserted
            {
                User FullUser = new User();
                FullUser = user.ReadOne(user.Email);
                return Ok(FullUser);
            }
            else
            {
                return BadRequest("User could not be added. Email or username might not be unique.");
            }
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn([FromQuery] string email, [FromQuery] string password)
        {
            //if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            //{
            //    return BadRequest("Email and password are required");
            //}

            User user = new User { Email = email, Password = password };
            User loggedInUser = user.LogIn(); 

            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }
            else
            {
                return NotFound("User not found or invalid credentials");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{email}")]
        public IActionResult Put([FromBody] User user)
        {
            //user.Email = email;
            if (user.Update() == 1) // Assuming Update() updates a user and returns the number of affected rows
            {
                User FullUser = new User();
                FullUser = user.ReadOne(user.Email);
                return Ok(FullUser);
            }
            else
            {
                return BadRequest("User not updated");
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{email}")]
        public IActionResult Delete([FromBody] User user)
        {
            //user.Email = email;
            if (user.Delete() == true) // Assuming Delete() deletes a user and returns the number of affected rows
            {
                return Ok("User deleted successfully");
            }
            else
            {
                return NotFound("User not found or not deleted");
            }
        }
    }
}


//