using WebAPI.Domain.Models;

namespace StudentApp.Services;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserInfo newUser);
    Task<bool> LoginAsync(UserInfo user);
}