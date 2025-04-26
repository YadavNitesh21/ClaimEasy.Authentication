using AuthenticationService.DTOs;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthUser> RegisterAsync(RegisterUserDTO registerUser);
        Task<AuthUser> GetUserByIdAsync(Guid userId);
        Task<List<AuthUser>> GetAllUserAsync();
        Task<AuthUser> GetUserByUsernameAsync(string username);
    }
}
