using Microsoft.AspNetCore.Identity;
using Sales.Shared.Entities;
using Sales.Shared.SecurityDtos;
using System.Collections.Generic;

namespace Sales.API.Helpers
{
    public interface IUserHelper
    {
        Task<IReadOnlyList<IdentityRole>> GetRolesList();
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task<bool> FindRoleByName(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDto model);
        Task LogoutAsync();

    }
}
