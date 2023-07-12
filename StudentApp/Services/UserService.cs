using System.Text.Json;
using StudentApp.Services.Interfaces;
using WebAPI.Domain.Models;

namespace StudentApp.Services;

public class UserService : IUserService
{
    private readonly IAuthenticationService _authenticationService;

    public UserService(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<bool> RegisterUserAsync(UserInfo newUser)
    {
        return await _authenticationService.RegisterUserAsync(newUser);
    }

    public async Task<bool> LoginAsync(UserInfo user)
    {
        return await _authenticationService.LoginAsync(user);
    }
}