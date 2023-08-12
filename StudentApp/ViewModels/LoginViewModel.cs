using StudentApp.Services;
using StudentApp.Services.Interfaces;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public UserInfo User { get; set; } = new();
    private readonly IAuthenticationService _authenticationService;

    public LoginViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<bool> TryLoginAsync()
    {
        if (User.Username is null | User.Password is null)
        {
            return false;
        }
        return await _authenticationService.LoginAsync(User);
    }
}