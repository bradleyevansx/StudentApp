using WebAPI.Domain.Models;

namespace StudentApp.Services.Interfaces;

public interface IAuthenticationService
{
    Task<bool> RegisterUserAsync(UserInfo newUser);
    Task<bool> TryRefreshAsync();
    Task<bool> LoginAsync(UserInfo user);
}