using Microsoft.AspNetCore.Identity;
using Sales.Shared.Entities;
using Sales.Shared.SecurityDtos;

namespace Sales.API.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDto model);
        Task LogoutAsync();

    }
}
