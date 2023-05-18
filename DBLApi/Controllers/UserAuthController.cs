using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserAuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Registration registrationRequest)
        {
            if (await _userRepository.IsEmailTaken(registrationRequest.Email))
            {
                return BadRequest(new {message = "Email is already taken"});
            }

            if (await _userRepository.IsUsernameTaken(registrationRequest.Username))
            {
                return BadRequest(new {message = "Username is already taken"});
            }

            var user = new User
            {
                Email = registrationRequest.Email,
                Username = registrationRequest.Username,
                Password = HashPassword(registrationRequest.Password)
            };

            await _userRepository.Add(user);
            return Ok(new {message = "User registered successfully"});
        }
    }
}