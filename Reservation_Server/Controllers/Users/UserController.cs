/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<Response> Post([FromBody] User user)
        {
            var result = userService.Create(user);

            Response userResponse = new()
            {
                message = result
            };
            return userResponse;


        }


        [HttpPut("{nic}")]
        public ActionResult<Response> Put(string nic, [FromBody]User user)
        {
            var existingUser = userService.Get(nic);

            if (existingUser == null)
            {
                Response userResponse = new()
                {
                    message = $"User not found"
                };
                return NotFound(userResponse);
            }

            userService.Update(nic, user);

            Response userResponse1 = new()
            {
                message = $"User updated successfully"
            };

            return Ok(userResponse1);
            
        }

        [HttpPatch("active_deactive/{nic}")]
        public ActionResult<Response> UpdateStatus(string nic)
        {

            string result = userService.UpdateStatus(nic);

            Response userResponse = new()
            {
                message = result
            };
            return Ok(userResponse);
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
                return user;
            }
            else if (isPasswordVerified == "deactivated")
            {
                Response userResponse = new()
                {
                    message = "Your Account Deactivated. Please contact us to Reactivate"
                };
                return BadRequest(userResponse);
            }
            else
            {
                Response userResponse = new()
                {
                    message = "Incorrect Nic or password"
                };
                return BadRequest(userResponse);

            }

        }
    }
}
