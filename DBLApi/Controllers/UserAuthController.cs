using DBLApi.Errors;
using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using DBLApi.Services;
using DBLApi.Services.Interfaces;
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
        private readonly IPasswordService _passwordService;

        public UserAuthController(IUserRepository userRepository, JwtTokenService jwtTokenService, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _passwordService = passwordService;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Registration registrationRequest)
        {
            if (await _userRepository.IsEmailTaken(registrationRequest.Email))
            {
                throw new UserAlreadyExistsException(registrationRequest.Email);
            }

            if (await _userRepository.IsUsernameTaken(registrationRequest.Username))
            {
                throw new UserAlreadyExistsException(registrationRequest.Username);
            }

            var user = new User
            {
                Email = registrationRequest.Email,
                Username = registrationRequest.Username,
                Salt = _passwordService.GenerateSalt(),
                Password = registrationRequest.Password,
                Birthday = registrationRequest.Birthday
            };

            user.Password = _passwordService.HashPassword(registrationRequest.Password, user.Salt);

            await _userRepository.Add(user);

            return Ok(new {message = "User registered successfully"});
        }
        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Authentication loginRequest)
        {
            var user = await _userRepository.GetUserByUsername(loginRequest.Username);

            if (user == null)
            {
                throw new UserNotFoundException(loginRequest.Username);
            }

            if (!_passwordService.VerifyPassword(loginRequest.Password, user.Salt, user.Password))
            {
                throw new InvalidPasswordException();
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

        [HttpPost("delete"), Authorize]
        public async Task<IActionResult> Delete([FromBody] Authentication loginRequest)
        {
            var user = await _userRepository.GetUserByUsername(loginRequest.Username);

            if (user == null)
            {
                throw new UserNotFoundException(loginRequest.Username);
            }

            if (!_passwordService.VerifyPassword(loginRequest.Password, user.Salt, user.Password))
            {
                throw new InvalidPasswordException();
            }

            await _userRepository.Delete(user);

            return Ok(new {message = "User deleted successfully"});
        }
    }
}