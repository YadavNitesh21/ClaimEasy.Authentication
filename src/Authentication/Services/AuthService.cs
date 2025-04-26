using Authentication.DBContext;
using Authentication.Helper;
using Authentication.Services.Interfaces;
using AuthenticationService.DTOs;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _dbContext;
        public AuthService(AuthDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<AuthUser> RegisterAsync(RegisterUserDTO register)
        {
            var salt = PasswordHelper.GenerateSalt();
            var passwordHash = PasswordHelper.HashPassword(register.Password, salt);
            var user = new AuthUser
            {
                Username = register.Username,
                PasswordHash = passwordHash,
                Salt = salt,
                Email = register.Email,
                Phone = register.Phone
            };

            _dbContext.AuthUsers.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<AuthUser> GetUserByUsernameAsync(string username)
        {
            if (username == null)
            {
                throw new KeyNotFoundException("Username not found");
            }
            return await _dbContext.AuthUsers
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<AuthUser> GetUserByIdAsync(Guid userId)
        {
            var user = await _dbContext.AuthUsers.FirstOrDefaultAsync(u => u.Id ==userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
        public async Task<List<AuthUser>> GetAllUserAsync()
        {
            return await _dbContext.AuthUsers.ToListAsync();

        }

    }
}
