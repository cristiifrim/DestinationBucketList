using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using DBLApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;

        public UserAuthController(IUserRepository userRepository, JwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register"), AllowAnonymous]
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
                Salt = BCrypt.Net.BCrypt.GenerateSalt(),
                Password = registrationRequest.Password,
                Birthday = registrationRequest.Birthday
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(registrationRequest.Password, user.Salt);

            await _userRepository.Add(user);
            return Ok(new {message = "User registered successfully"});
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Authentication loginRequest)
        {
            var user = await _userRepository.GetUserByUsername(loginRequest.Username);

            if (user == null)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginRequest.Password, user.Salt);
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(hashedPassword, user.Password);

            if (isPasswordValid)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                role = user.Role,
                token
            });
        }
    }


}