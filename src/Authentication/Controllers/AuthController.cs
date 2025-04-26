using System.Security.Cryptography;
using System.Text;
using Authentication.DTOs;
using Authentication.Helper;
using Authentication.Services.Interfaces;
using AuthenticationService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtHelper _jwtHelper;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            var user = await _authService.RegisterAsync(dto);
            return Ok(new { user.Id, user.Username, user.Email, user.Phone });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.GetUserByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtHelper.GenerateToken(user);
            return Ok(new { Token = token });
        }

        private bool VerifyPassword(string inputPassword, string storedPasswordHash, string storedSalt)
        {
            // Hash the input password with the stored salt
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = inputPassword + storedSalt;
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Compare the hashed password with the stored hash
                return hashString == storedPasswordHash;
            }
        }
    }
}
