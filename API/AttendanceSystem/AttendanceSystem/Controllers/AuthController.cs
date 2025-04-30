using AttendanceSystem.DTOs.Auth;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public AuthController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestPayload data) {
            if (data == null || string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var user = _context.Users.FirstOrDefault(u => u.Username == data.Username);
            if (user == null)
            {
                return NotFound("User Not Found.");
            }

            if(user.PasswordHash != data.Password)
            {
                return Unauthorized("Invalid Credentials");
            }
            var master_user = _context.Usermasters.FirstOrDefault(u => u.MasterId == user.UserId);
            var response = new LoginResponsePayload
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = master_user.Role,
                JWTtoken = "Sample_token" // Need to fill this for JWT
            };
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegsitrationRequest data)
        {
            if(data == null || string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password) || data.UserId <= 0)
            {
                return BadRequest(new { message = "Invalid registration request." });
            }
            var alreadyRegistered = _context.Users.FirstOrDefault(u => u.UserId == data.UserId);
            if (alreadyRegistered != null)
            {
                return Ok(new { message = "User already registered." });
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == data.Username);
            if (existingUser != null)
            {
                return Ok(new { message = "Username already exists." });
            }
            var existingMaster = _context.Usermasters.FirstOrDefault(u => u.MasterId == data.UserId);
            if(existingMaster == null)
            {
                return Ok(new { message = "User records not found. Please Contact the administrative office." });
            }
            // create a password hash

            var newUser = new User
            {
                UserId = data.UserId,
                Username = data.Username,
                PasswordHash = data.Password, //replace it with hashed password
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(new { message = "User Registered Successfully!!"});
        }
    }
}
