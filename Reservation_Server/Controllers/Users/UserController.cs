﻿using Microsoft.AspNetCore.Mvc;
using Reservation_Server.Models.Users;
using Reservation_Server.Services.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation_Server.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;

        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return userService.Get();
        }

        [HttpGet("{nic}")]
        public ActionResult<User> Get(string nic)
        {
            var user = userService.Get(nic);

            if (user == null)
            {
                return NotFound($"User with Nic {nic} not found");
            }
            return user;
        }


        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            var result = userService.Create(user);

            return Ok(result);
        }


        [HttpPut("{nic}")]
        public ActionResult Put(string nic, [FromBody]User user)
        {
            var existingUser = userService.Get(nic);

            if (existingUser == null)
            {
                return NotFound($"User with nic = {nic} not found");
            }

            userService.Update(nic, user);
            return Ok("Updated");
        }

        [HttpPatch("active_deactive/{nic}")]
        public ActionResult UpdateStatus(string nic)
        {

            var result = userService.UpdateStatus(nic);
            return Ok(result);
        }

        [HttpDelete("{nic}")]
        public ActionResult Delete(string nic)
        {

            var user = userService.Get(nic);

            if (user == null)
            {
                return NotFound($"User with Nic = {nic} not found");
            }
            userService.Delete(user.Nic);

            return Ok($"User with Nic = {nic} deleted");
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest loginRequest)
        {
            var user = userService.Get(loginRequest.Nic);

            if (user == null)
            {
                return NotFound($"User with Nic {loginRequest.Nic} not found");
            }

            var isPasswordVerified = userService.Login(loginRequest.Nic, loginRequest.Password);

            if (isPasswordVerified == "true")
            {

                // Create a new user object without the password
                var userWithoutPassword = new User
                {
                    Nic = user.Nic,
                    Name = user.Name,
                    Email = user.Email,
                    IsTraveler = user.IsTraveler,
                    IsAgent = user.IsAgent,
                    IsBackOffice = user.IsBackOffice,
                    IsActive = user.IsActive
                };

                return userWithoutPassword;
            }
            else if (isPasswordVerified == "deactivated")
            {
                return BadRequest("deactivated");
            }
            else
            {
                return BadRequest("Incorrect Nic or password");

            }

        }
    }
}
